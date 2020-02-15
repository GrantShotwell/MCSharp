using LargeBaseNumbers;
using MCSharp.Compilation;
using MCSharp.Methods;
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
        public static StreamWriter DemoFunction { get; private set; }
        public static StreamWriter TickFunction { get; private set; }
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

        public static Dictionary<string, Access> AccessModifiers { get; } = new Dictionary<string, Access>() {
            { "public", Access.Public },
            { "private", Access.Private }
        };
        public static Dictionary<string, Usage> UsageModifiers { get; } = new Dictionary<string, Usage>() {
            { "static", Usage.Static },
            { "const", Usage.Constant }
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
            DemoFunction = File.CreateText(functionsPath + "\\mcscript\\demo.mcfunction");
            TickFunction = File.CreateText(functionsPath + "\\mcscript\\tick.mcfunction");

            foreach(string scriptPath in scripts) {

                string scriptName = scriptPath.Split('\\')[^1];
                string functionName = scriptName.Replace(".mcsharp", ".mcfunction");
                string functionPath = $"{functionsPath}\\{functionName}";
                if(!scriptPath.EndsWith(".mcsharp")) continue;

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Compiling '{scriptName}'... ");

                using(StreamReader reader = File.OpenText(scriptPath)) {
                    var scriptFile = new ScriptFile(scriptPath, reader.ReadToEnd().Replace('\n', ' ').Replace('\t', ' ').Replace('\r', ' '));
                    foreach(ScriptClass scriptClass in scriptFile) foreach(ScriptFunction scriptFunction in scriptClass)
                            WriteFunction(scriptFunction.FilePath, scriptFunction.FileName, RootScope, scriptFunction);
                }

                Console.CursorTop -= maxFunctionStackSize + 1;
                Program.ClearCurrentConsoleLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Compiled '{scriptPath.Split('\\')[^1]}'.");
                Console.CursorTop += maxFunctionStackSize;

                maxFunctionStackSize = 0;

            }

            VariableScopes.TryGetValue(RootScope, out List<Variable> publicVariables);
            var allVariables = new List<Variable>();
            foreach(KeyValuePair<Scope, List<Variable>> pair in VariableScopes)
                foreach(Variable variable in pair.Value) allVariables.Add(variable);
            allVariables.Sort((a, b) => a.Order - b.Order);

            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Compiling 'mcsript\\prep.mcfunction'...");

                PrepFunction.WriteLine($"function {Program.Datapack.Name}:mcscript/demo");
                if(publicVariables != null) foreach(Variable variable in publicVariables) variable.WritePrep(PrepFunction);
                PrepFunction.WriteLine($"function {Program.Datapack.Name}:program/load");

                PrepFunction.Close();

                Program.ClearCurrentConsoleLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Compiled 'mcsript\\prep.mcfunction'.");
            }

            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Compiling 'mcscript\\tick.mcfunction'...");

                TickFunction.WriteLine($"function {Program.Datapack.Name}:program/main");
                foreach(Variable variable in allVariables) variable.WriteTick(TickFunction);

                TickFunction.Close();

                Program.ClearCurrentConsoleLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Compiled 'mcsript\\tick.mcfunction'.");
            }

            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Compiling 'mcscript\\demo.mcfunction'...");

                if(publicVariables != null) foreach(Variable variable in publicVariables) variable.WriteDemo(DemoFunction);

                DemoFunction.Close();

                Program.ClearCurrentConsoleLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Compiled 'mcsript\\demo.mcfunction'.");
            }

        }

        /// <summary>
        /// Compiles a new function file with the given <paramref name="functionPath"/> from the given <paramref name="script"/>.
        /// </summary>
        public static void WriteFunction(string path, string alias, Scope scope, ScriptFunction script) {

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($" · Writing '{alias}'... ");
            maxFunctionStackSize++;

            string[] directorySplit = path.Split('\\')[..^1];
            string directory = "";
            foreach(string part in directorySplit) directory += '\\' + part;
            Directory.CreateDirectory(directory[1..]);
            StreamWriter writer = File.CreateText(path.Split("(", 2)[0]);
            FunctionStack.Push(writer);

            CurrentScope = new Scope(scope);
            ScopeStack.Push(CurrentScope);

            //

            var phrases = (IReadOnlyList<ScriptLine>)script;
            for(int i = 0; i < phrases.Count; i++) {
                var phrase = phrases[i];

                var arguments = new List<ScriptWild>();
                Action onFinish = null;

                var wilds = (IReadOnlyList<ScriptWild>)phrase;
                for(int j = 0; j < wilds.Count; j++) {
                    var wild = wilds[j];

                    if(onFinish == null) {
                        if(wild.IsWord) {
                            ScriptWord word = wild.Word;
                            string wordStr = word;
                            
                            //Check for: [TYPE]
                            if(Variable.Compilers.TryGetValue(word, out Func<Access, Usage, string, Scope, ScriptWild[], Variable> compiler)) {
                                onFinish = () => {
                                    compiler.Invoke(Access.Private, Usage.Default, arguments[0], CurrentScope, arguments.ToArray()[1..]);
                                };
                                continue;
                            }

                            //Check for: [NAME]
                            if(TryGetVariable(word, CurrentScope, out Variable variable)) {
                                onFinish = () => {
                                    variable.CompileOperation(arguments[0].Word, arguments.ToArray()[1..]);
                                };
                                continue;
                            }
                            
                            //Check for: [STATIC METHOD]
                            if(MethodGroup.Dictionary.TryGetValue(word, out Action<Variable[]> stcMethod)) {
                                onFinish = () => {
                                    #region Call Static Method

                                    //Check that 'arguments' are method arguments.
                                    if(arguments.Count != 1 && arguments[0].IsWord && arguments[0].BlockType != "(\\)")
                                        throw new SyntaxException("Expected arguments for a static method.");

                                    //Translate 'arguments' into a Variable array.
                                    int i = 0;
                                    var variables = new Variable[arguments[0].Wilds.Count];
                                    if(variables.Length > 0) {
                                        foreach(string name in ((string)arguments[0]).Split(',')) {
                                        if(!TryGetVariable(name.Trim(), CurrentScope, out variables[i++]))
                                            throw new SyntaxException("Expected an initialized variable as argument for static method.");
                                        }
                                    }

                                    //Invoke the method.
                                    stcMethod.Invoke(variables);

                                    #endregion
                                };
                                continue;
                            }

                            //Check for: [VARIABLE METHOD]
                            if(TryGetVariable(wordStr.Split('.', 2)[0], CurrentScope, out variable)) {
                                int stop;
                                for(stop = 0; stop < wordStr.Length; stop++) if(wordStr[stop] == '(') break;
                                string call = wordStr[variable.ObjectName.Length..(stop)];
                                foreach(MethodGroup.GenericDictionary genericDictionary in MethodGroup.GenericDictionaries) {
                                    if(genericDictionary.Type.IsAssignableFrom(variable.GetType())) {
                                        foreach(KeyValuePair<string, Action<Variable, Variable[]>> pair in genericDictionary) {
                                            if(pair.Key == call) {
                                                var varMethod = pair.Value;
                                                onFinish = () => {
                                                    #region Call Non-Static Method

                                                    //Check that 'arguments' are method arguments.
                                                    if(arguments.Count != 1 && arguments[0].IsWord && arguments[0].BlockType != "(\\)")
                                                        throw new SyntaxException("Expected arguments for a non-static method.");

                                                    //Translate 'arguments' into a Variable array.
                                                    int i = 0;
                                                    var variables = new Variable[arguments[0].Wilds.Count];
                                                    if(variables.Length > 0) {
                                                        foreach(string name in ((string)arguments[0]).Split(',')) {
                                                            if(!TryGetVariable(name.Trim(), CurrentScope, out variables[i++]))
                                                                throw new SyntaxException("Expected an initialized variable as argument for non-static method.");
                                                        }
                                                    }

                                                    //Invoke the method.
                                                    varMethod.Invoke(variable, variables);

                                                    #endregion
                                                };
                                                goto BreakAll;
                                            }
                                        }
                                    }
                                    continue;
                                    BreakAll: break;
                                }
                                continue;
                            }


                            throw new SyntaxException($"Unexpected keyword '{(string)wild}'.");

                        } else {

                            throw new SyntaxException($"Unexpected block '{(string)wild}'.");

                        }
                    } else {

                        arguments.Add(wild);

                    }
                }

                if(onFinish != null) onFinish.Invoke();

            }

            if(VariableScopes.TryGetValue(CurrentScope, out List<Variable> variables)) {
                foreach(Variable variable in variables) variable.WriteInit(FunctionStack.Peek());
                foreach(Variable variable in variables) variable.WriteDemo(FunctionStack.Peek());
            }

            //

            FunctionStack.Pop().Close();
            ScopeStack.Pop();

            Program.ClearCurrentConsoleLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" · Wrote '{alias}'.");

        }

        public static bool WhatIsThis(ScriptWild wild, string name, out object result) {

            result = null;

            //todo

            return false;

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

        public static bool TryGetVariable(string name, Scope scope, out Variable variable) {
            variable = null;
            if(VariableNames.TryGetValue(name, out Dictionary<Scope, Variable> dictionary)) {
                int min = int.MaxValue;
                foreach(KeyValuePair<Scope, Variable> pair in dictionary) {
                    Scope s = pair.Key;
                    Variable v = pair.Value;
                    scope.IsChildOf(s, out int d);
                    if(d < min) {
                        variable = v;
                        min = d;
                    }
                }
            }
            return variable != null;
        }

        private static void Reload() {

            //Clear files from last compile.
            ScriptFile.Files.Clear();

            //Clear compilers from last compile.
            Variable.Compilers.Clear();

            //Clear method groups from last compile.
            MethodGroup.Dictionary.Clear();
            foreach(var dictionary in MethodGroup.GenericDictionaries)
                dictionary.Clear();

            //Clear datatypes from last compile.
            Datatypes.Clear();

            //Reset objective ids.
            VarObjective.ResetID();

            //Clear scopes from last compile.
            allScopes.Clear();
            allScopes.Add(RootScope);
            ScopeStack.Clear();

            //Clear stack from last compile.
            FunctionStack.Clear();

            //Clear variables from last compile.
            VariableNames.Clear();
            VariableScopes.Clear();

            //Get executing assembly.
            var assembly = Assembly.GetExecutingAssembly();

            //Find the Variable types from this assembly, and add them to 'Datatypes'.
            foreach(TypeInfo info in assembly.DefinedTypes) {
                if(info.IsSubclassOf(typeof(Variable)) && !info.IsAbstract) {
                    ConstructorInfo constructor = info.GetConstructor(new Type[] { });
                    var variable = constructor.Invoke(new object[] { }) as Variable;
                    if(!(variable is Spy)) Datatypes.Add(variable.TypeName, info.AsType());
                }
            }

            //Find the MethodGroup types from this assembly, and call their empty constructor.
            foreach(TypeInfo info in assembly.DefinedTypes) {
                if(info.IsSubclassOf(typeof(MethodGroup)) && !info.IsAbstract) {
                    ConstructorInfo constructor = info.GetConstructor(new Type[] { });
                    constructor.Invoke(new object[] { });
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

            public bool IsChildOf(Scope scope, out int delta) {
                Scope parent = Parent;
                delta = -1;
                while(parent != null) {
                    delta++;
                    if(scope == parent) return true;
                    else parent = parent.Parent;
                }
                return false;
            }

            public bool IsParentOf(Scope scope, out int delta) {
                static bool Recursion(Scope me, Scope scope, ref int delta) {
                    delta++;
                    foreach(Scope child in me.Children) {
                        if(scope == child) return true;
                        else if(Recursion(child, scope, ref delta)) return true;
                    }
                    return false;
                }
                delta = -1;
                return Recursion(this, scope, ref delta);
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
