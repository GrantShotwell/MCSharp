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

        public static StreamWriter PrepFunction { get; private set; }
        public static StreamWriter UndoFunction { get; private set; }
        public static Stack<StreamWriter> FunctionStack { get; } = new Stack<StreamWriter>();
        public static Dictionary<string, Dictionary<string, Variable>> Variables { get; } = new Dictionary<string, Dictionary<string, Variable>>();

        public static Dictionary<string, string> ScopeIDs { get; } = new Dictionary<string, string>();
        public static string CurrentScope { get; private set; }

        public static string CurrentLine { get; private set; }
        public static int CurrentLineIndex { get; private set; }
        public static string CurrentWord { get; private set; }
        public static int CurrentWordIndex { get; private set; }
        public static string CurrentFile { get; private set; }

        public static HashSet<string> Modifiers { get; } = new HashSet<string>() { "public", "private", "const" };
        public static Dictionary<string, Type> Datatypes { get; } = new Dictionary<string, Type>();
        public static Dictionary<string, OperatorType> Operators { get; } = new Dictionary<string, OperatorType>() {
            { "=", OperatorType.Set },
            { "+", OperatorType.Primitive },
            { "-", OperatorType.Primitive },
            { "*", OperatorType.Primitive },
            { "/", OperatorType.Primitive },
            { "%", OperatorType.Primitive },
            { "+=", OperatorType.SetPrimitive },
            { "-=", OperatorType.SetPrimitive },
            { "*=", OperatorType.SetPrimitive },
            { "/=", OperatorType.SetPrimitive },
            { "%=", OperatorType.SetPrimitive }
        };
        
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
                if(!scriptPath.EndsWith(".mcsharp")) continue;

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"Compiling '{scriptName}'... ");

                StreamWriter writer = File.CreateText(functionPath);
                FunctionStack.Push(writer);
                using StreamReader reader = File.OpenText(scriptPath);
                string script = reader.ReadToEnd().Replace('\n', ' ');

                string[] lines = script.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for(CurrentLineIndex = 0; CurrentLineIndex < lines.Length; CurrentLineIndex++) {
                    CurrentLine = lines[CurrentLineIndex].Trim();

                    string[] split = CurrentLine.Split(new char[] { ' ' });
                    for(CurrentWordIndex = 0; CurrentWordIndex < split.Length; CurrentWordIndex++) {
                        CurrentWord = split[CurrentWordIndex];
                        bool lineEnd = CurrentWordIndex == split.Length - 1;

                        if(Variable.Compilers.TryGetValue(CurrentWord, out Action<string, string, string, string[]> compiler)) {
                            string[] chop = CurrentLine.Split(CurrentWord, 2);
                            bool hasModifier = chop.Length == 2;

                            //Format here so it's easier to write the Compile() functions.

                            string[] arguments = hasModifier ? chop[1].Split(' ')[1..] : chop[0].Split(' ');
                            string modifier = hasModifier ? chop[0] : "";
                            string objectName = arguments[0];
                            string scope = "global"; //CurrentScope;

                            compiler.Invoke(modifier, objectName, scope, arguments[1..]);

                        }

                    }

                }

                Program.ClearCurrentConsoleLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Compiled '{scriptPath.Split('\\')[^1]}'.");

            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Compiling 'mcsript\\prep.mcfunction'...");

            //todo: add required order - add scope object?
            foreach(KeyValuePair<string, Dictionary<string, Variable>> pair in Variables) {
                string name = pair.Key;
                Dictionary<string, Variable> variables = pair.Value;
                if(variables.TryGetValue("global", out Variable variable)) {
                    variable.Initialize(true);
                }
            }

            PrepFunction.Close();
            UndoFunction.Close();

            Program.ClearCurrentConsoleLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Compiled 'mcsript\\prep.mcfunction'.");

        }

        public static void AddVariable(Variable variable) {
            if(!Variables.TryGetValue(variable.ObjectName, out Dictionary<string, Variable> variables)) {
                //The item doesn't exist yet. Make it.
                variables = new Dictionary<string, Variable>();
                Variables.Add(variable.ObjectName, variables);
            }
            //Whether we just made it or just found it, add the variable to the dictionary.
            variables.Add(variable.Scope, variable);
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

            //Clear stack from last compile.
            FunctionStack.Clear();

            //Clear variables from last compile.
            Variables.Clear();

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

        class SyntaxNode {

            private readonly List<SyntaxNode> children = new List<SyntaxNode>();
            private SyntaxNode parent;

            public SyntaxType Type { get; set; }
            public string Value { get; set; }
            public IReadOnlyCollection<SyntaxNode> Children => children;

            public SyntaxNode Parent {
                get => parent;
                set {
                    parent?.children.Remove(this);
                    value?.children.Add(this);
                    parent = value;
                }
            }

        }

        public class SyntaxException : Exception {
            public SyntaxException(string message) : base(message) { }
        }

    }

}
