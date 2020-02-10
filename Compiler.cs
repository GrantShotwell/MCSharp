using LargeBaseNumbers;
using MCSharp.Compilation;
using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MCSharp {

    public static class Compiler {

        public enum TraceFormat { Sentence, Phrase, CapitalizedPhrase }
        public enum OperatorType { Set, Primitive, SetPrimitive }
        public enum SyntaxType { Modifier, Datatype, Operator, Name, IncreaseScope, DecreaseScope }

        private static int maxFunctionStackSize = 0;

        private static readonly Dictionary<int, Scope> allScopes = new Dictionary<int, Scope>();

        public static Scope RootScope { get; } = new Scope(0, null);

        public static StreamWriter PrepFunction { get; private set; }
        public static StreamWriter UndoFunction { get; private set; }
        public static Stack<StreamWriter> FunctionStack { get; } = new Stack<StreamWriter>();
        public static Dictionary<string, Dictionary<Scope, Variable>> VariableNames { get; } = new Dictionary<string, Dictionary<Scope, Variable>>();
        public static Dictionary<Scope, List<Variable>> VariableScopes { get; } = new Dictionary<Scope, List<Variable>>();

        public static IReadOnlyDictionary<int, Scope> AllScopes => allScopes;
        public static Scope CurrentScope { get; private set; }
        public static Stack<Scope> ScopeStack { get; } = new Stack<Scope>();

        public static string CurrentLine { get; private set; }
        public static int CurrentLineIndex { get; private set; }
        public static string CurrentWord { get; private set; }
        public static int CurrentWordIndex { get; private set; }
        public static string CurrentFile { get; private set; }

        public static Dictionary<string, AccessModifier> AccessModifiers { get; } = new Dictionary<string, AccessModifier>() {
            { "public", AccessModifier.Public },
            { "private", AccessModifier.Private }
        };
        public static Dictionary<string, UsageModifier> UsageModifiers { get; } = new Dictionary<string, UsageModifier>() {
            { "static", UsageModifier.Static },
            { "const", UsageModifier.Constant }
        };
        public static Dictionary<string, Type> Datatypes { get; } = new Dictionary<string, Type>();
        
        public static void Compile(string directory) {

            //Clear and reset saved values from last compile.
            Reload();

            string functionsPath = directory + "\\functions";
            string[] functions = Directory.GetFiles(functionsPath);
            string scriptsPath = directory + "\\scripts";
            string[] scripts = Directory.GetFiles(scriptsPath);

            Directory.CreateDirectory(functionsPath + "\\mcscript");
            PrepFunction = File.CreateText(functionsPath + "\\mcscript\\prep.mcfunction");
            UndoFunction = File.CreateText(functionsPath + "\\mcscript\\undo.mcfunction");


            foreach(string scriptPath in scripts) {

                string scriptName = scriptPath.Split('\\')[^1];
                string functionName = scriptName.Replace(".mcsharp", ".mcfunction");
                string functionPath = $"{functionsPath}\\{functionName}";
                if(!scriptPath.EndsWith(".mcsharp")) return;

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Compiling '{scriptName}'... ");

                using(StreamReader reader = File.OpenText(scriptPath)) {
                    var script = new Script(scriptPath, reader.ReadToEnd().Replace('\n', ' ').Replace('\t', ' ').Replace('\r', ' '));
                    WriteFunction(functionPath, functionName, script);
                }

                Console.CursorTop -= maxFunctionStackSize;
                Program.ClearCurrentConsoleLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Compiled '{scriptPath.Split('\\')[^1]}'.");
                Console.CursorTop += maxFunctionStackSize;

                maxFunctionStackSize = 0;

            }


            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Compiling 'mcsript\\prep.mcfunction'...");

            if(VariableScopes.TryGetValue(RootScope, out List<Variable> variables)) {
                variables.Sort((a, b) => a.Order - b.Order);
                foreach(Variable variable in variables) {
                    variable.Initialize(true);
                }
            }

            PrepFunction.Close();
            UndoFunction.Close();

            Program.ClearCurrentConsoleLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Compiled 'mcsript\\prep.mcfunction'.");

        }

        /// <summary>
        /// Compiles a new function file with the given <paramref name="functionPath"/> from the given <paramref name="script"/>.
        /// </summary>
        public static void WriteFunction(string functionPath, string functionName, Script script) {

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($" - Writing '{functionName}'... ");
            maxFunctionStackSize++;

            StreamWriter writer = File.CreateText(functionPath);
            FunctionStack.Push(writer);

            CurrentScope = new Scope(RootScope);
            ScopeStack.Push(CurrentScope);

            //

            var phrases = (IReadOnlyList<Phrase>)script;
            for(int i = 0; i < phrases.Count; i++) {
                var phrase = phrases[i];

                bool sendingMode = false;

                var accessModifiers = new List<AccessModifier>();
                var usageModifiers = new List<UsageModifier>();
                var arguments = new List<Wild>();

                Action<AccessModifier[], UsageModifier[], string, Scope, Wild[]> compiler = null;

                var wilds = (IReadOnlyList<Wild>)phrase;
                for(int j = 0; j < wilds.Count; j++) {
                    var wild = wilds[j];

                    if(sendingMode) {

                        arguments.Add(wild);

                    } else if(wild.IsWord) {
                        Word word = wild.Word;

                        //Check for keywords: [MODIFIER]
                        if(AccessModifiers.TryGetValue(word, out AccessModifier modifier1)) {
                            accessModifiers.Add(modifier1);
                            continue;
                        }
                        if(UsageModifiers.TryGetValue(word, out UsageModifier modifier2)) {
                            usageModifiers.Add(modifier2);
                            continue;
                        }

                        //Check for keywords: [VARIABLE]
                        if(Variable.Compilers.TryGetValue(word, out compiler)) {
                            sendingMode = true;
                            continue;
                        }

                    } else {

                        //Should not get to a Wild.IsWild if not in sending mode!
                        throw new SyntaxException("Compiler expected a variable keyword.");

                    }
                }

                if(sendingMode) compiler.Invoke(accessModifiers.ToArray(), usageModifiers.ToArray(), arguments[0],
                                                accessModifiers.Contains(AccessModifier.Public) ? RootScope : CurrentScope,
                                                arguments.ToArray()[1..]);

            }

            if(VariableScopes.TryGetValue(CurrentScope, out List<Variable> variables)) {
                variables.Sort((a, b) => a.Order - b.Order);
                foreach(Variable variable in variables) {
                    variable.Initialize(false);
                }
            }

            //

            FunctionStack.Pop().Close();
            ScopeStack.Pop();

            Program.ClearCurrentConsoleLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" - Wrote '{functionName}'.");

        }

        public static string GetCurrentLocationTrace(TraceFormat format) {
            switch(format) {
                case TraceFormat.Sentence:
                    break;
                case TraceFormat.Phrase:
                    break;
                case TraceFormat.CapitalizedPhrase:
                    return $"Line {CurrentLineIndex} in '{CurrentFile}'.";
            }
            throw new ArgumentException($"The given argument is not a {nameof(TraceFormat)}.", nameof(format));
        }

        private static void Reload() {

            //Clear compilers from last compile.
            Variable.Compilers.Clear();

            //Clear datatypes from last compile.
            Datatypes.Clear();

            //Reset objective ids.
            Objective.ResetID();

            //Clear scopes from last compile.
            allScopes.Clear();
            allScopes.Add(RootScope);
            ScopeStack.Clear();

            //Clear stack from last compile.
            FunctionStack.Clear();

            //Clear variables from last compile.
            VariableNames.Clear();
            VariableScopes.Clear();

            //Find the Variable types from this assembly, and add them to 'Datatypes'.
            var assembly = Assembly.GetExecutingAssembly();
            foreach(TypeInfo info in assembly.DefinedTypes) {
                if(info.IsSubclassOf(typeof(Variable)) && !info.IsAbstract) {
                    ConstructorInfo constructor = info.GetConstructor(new Type[] { });
                    var variable = constructor.Invoke(new object[] { }) as Variable;
                    Datatypes.Add(variable.TypeName, info.AsType());
                }
            }

        }

        public class Scope {

            private readonly List<Scope> children = new List<Scope>();
            private Scope parent;

            public int ID { get; }
            public HashSet<Variable> Variables { get; } = new HashSet<Variable>(16);
            public IReadOnlyCollection<Scope> Children => children;
            public Scope Parent {
                get => parent;
                private set {
                    parent?.children.Remove(this);
                    value?.children.Add(this);
                    parent = value;
                }
            }


            public Scope(Scope parent) : this(GetNewID(), parent) { }

            public Scope(int id, Scope parent) {
                ID = id;
                Parent = parent;
                allScopes.Add(this);
            }

            public bool IsChildOf(Scope scope) {
                Scope parent = Parent;
                while(Parent != null) {
                    if(scope == parent) return true;
                    else parent = parent.Parent;
                }
                return false;
            }

            public bool IsParentOf(Scope scope) {
                foreach(Scope child in Children) {
                    if(scope == child) return true;
                    else if(child.IsParentOf(scope)) return true;
                }
                return false;
            }

            public static int GetNewID() {
                var random = new Random();
                int id = 0;
                while(AllScopes.ContainsKey(id)) id = random.Next(1, int.MaxValue - 1);
                return id;
            }

            public override string ToString() => BaseConverter.Convert(ID, 62);

            public override int GetHashCode() => ID;

        }

        public static void Add(this Dictionary<int, Scope> dictionary, Scope scope) => dictionary.Add(scope.ID, scope);

        public class SyntaxException : Exception {
            public SyntaxException(string message) : base(message) { }
        }

    }

}
