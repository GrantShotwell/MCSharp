using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Extensions;

namespace MCSharp.Compilation;

/// <summary>
/// Represents a virtual machine.
/// </summary>
public class VirtualMachine {

	public PrimitiveInstance.IntegerInstance.Constant ProgramId { get; set; }

	public PrimitiveInstance.IntegerInstance RuntimeId { get; set; }

	/// <summary>
	/// Creates an <see cref="PrimitiveInstance.IntegerInstance"/> with a random value at <paramref name="location"/>.
	/// </summary>
	/// <param name="location">The location of the <see cref="PrimitiveInstance.IntegerInstance"/>.</param>
	/// <returns>The <see cref="PrimitiveInstance.IntegerInstance"/>.</returns>
	public static PrimitiveInstance.IntegerInstance GenerateRandomIntegerInstance(Compiler.CompileArguments location) {

		FunctionWriter writer = location.Function.Writer;

		writer.WriteComments(
			"Generate a random integer.",
			indentBefore: false);
		PrimitiveInstance.IntegerInstance integer = location.Compiler.DefinedTypes[MCSharpLinkerExtension.IntIdentifier].InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
		string tag = "random_uuid";

		writer.WriteCommand($"summon area_effect_cloud ~ ~ ~ {{Tags:[\"{tag}\"]}}",
			"Create a temporary entity.");
		writer.WriteCommand($"execute store result score {MCSharpLinkerExtension.StorageSelector} {integer.Objective.Name} run data get entity @e[type=area_effect_cloud,tag={tag},limit=1] UUID[0] 1",
			"Store that entity's random UUID into an objective.");
		writer.WriteCommand($"kill @e[type=area_effect_cloud,tag={tag}]",
			"Remove the entity.",
			indentAfter: false);

		return integer;

	}

}
