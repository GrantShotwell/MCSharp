using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {
   
    public static class Script {

        public static string FixAlias(string alias) {
            var list = new List<char>(alias);
            for(int i = 0; i < list.Count; i++) {
                char current = list[i];
                if(char.IsUpper(current)) {
                    list[i] = char.ToLower(current);
                    if(i != 0 && list[i - 1] != '\\') list.Insert(i++, '-');
                }
            }
            return new string(list.ToArray());
        }

        public static void GetNames(string scriptPath, out string functionPath, out string functionName, out string functionAlias) {
            functionAlias = FixAlias(scriptPath[Program.ScriptsFolder.Length..^".mcscript".Length]);
            functionName = functionAlias + ".mcfunction";
            functionPath = Program.FunctionsFolder + functionName;
        }

    }

}
