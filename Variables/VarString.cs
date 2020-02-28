using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public class VarString : Variable {

        public override int Order => 100;
        public override string TypeName => "string";
        public string ConstantValue { get; }
        public VarSelector SelectorValue { get; }
        public bool IsConstant => ConstantValue != null;
        public bool IsSelector => SelectorValue != null;

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant };


        public VarString() : base() { }

        public VarString(Access access, string objectName, Compiler.Scope scope, string value) : base(access, Usage.Constant, objectName, scope) {
            ConstantValue = value;
        }

        public VarString(Access access, string objectName, Compiler.Scope scope, VarSelector value) : base(access, Usage.Constant, objectName, scope) {
            SelectorValue = value;
        }
        
        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            if(arguments.Length < 2 || arguments[0].IsWilds || (string)arguments[0].Word != "=")
                throw new Compiler.SyntaxException("Unexpected format for declaring a 'string'.", arguments[0].ScriptTrace);
            string value = CompileStringValue(arguments[1..]);
            return usage == Usage.Constant
                ? new VarString(access, objectName, scope, value)
                : new VarString(access, objectName, scope, CreateStringEntity(access, usage, objectName, scope, value));
        }

        public static VarSelector CreateStringEntity(Access access, Usage usage, string stringObjectName, Compiler.Scope scope, string value, bool auto = true) {
            string tag = $"{scope}.{stringObjectName}";
            new Spy(null, new string[] { $"execute as @p run summon minecraft:area_effect_cloud ~ ~ ~ " +
                $"{{CustomName:\"{(auto ? VarJSON.EscapeValue($"\\\"text\\\":\\\"{value}\\\"", 1) : value)}\"," +
                $"CustomNameVisible:0b,Tags:[\"{tag}\"],Particle:mobSpell}}" }, null);
            return new VarSelector(access, usage, $"{stringObjectName}.Selector", scope, $"@e[nbt={{Tags:[\"{tag}\"]}}]");
        }
        
        public static string CompileStringValue(ScriptWild[] arguments) {
            string value = ((string)new ScriptWild(arguments, "\"\\\"", ' ')).Trim();
            if(value[0] != '\"' || value[^1] != '\"') throw new Compiler.SyntaxException("Expected a string for declaring a string.", arguments[0].ScriptTrace);
            return value[2..^2];
        }

        public override void WriteTick(StreamWriter function) { if(IsSelector) function.WriteLine($"tp {SelectorValue.GetConstant()} @p[sort=arbitrary]"); }
        public override void WriteDemo(StreamWriter function) { if(IsSelector) function.WriteLine($"kill {SelectorValue.GetConstant()}"); }

        public override string GetConstant() => ConstantValue;
        public override VarString GetString() => this;

    }

}
