using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

	public class VirtualMachine {

		public PrimitiveInstance.IntegerInstance.Constant ProgramId { get; set; }

		public PrimitiveInstance.IntegerInstance RuntimeId { get; set; }

		public static PrimitiveInstance.IntegerInstance GenerateRandomIntegerInstance(Compiler.CompileArguments location) {

			FunctionWriter writer = location.Function.Writer;
			PrimitiveInstance.IntegerInstance integer = location.Compiler.DefinedTypes[MCSharpLinkerExtension.IntIdentifier].InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
			string randomTag = "random_uuid";

			writer.WriteCommand($"summon area_effect_cloud ~ ~ ~ {{ Tags: [\"{randomTag}\"]}}");
			writer.WriteCommand($"execute store result score {MCSharpLinkerExtension.StorageSelector} {integer.Objective} run data get entity @e[type = area_effect_cloud, tag = {randomTag}, limit = 1] UUID[0] 1");
			writer.WriteCommand($"kill @e[type = area_effect_cloud, tag = {randomTag}]");

			return integer;

		}

	}

}
