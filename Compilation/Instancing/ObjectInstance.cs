﻿using MCSharp.Linkage;
using MCSharp.Linkage.Extensions;
using MCSharp.Linkage.Minecraft;
using System;
using System.Diagnostics;

namespace MCSharp.Compilation.Instancing;

/// <summary>
/// Represents an instance of the base object class, similar to an instance of C#'s <see cref="object"/>.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public class ObjectInstance : IInstance {

	/// <summary>
	/// The name of the Minecraft objective used to store <see cref="Pointer"/>.
	/// </summary>
	public static string AddressObjectiveName => "mcs.object.id";

	/// <summary>
	/// The type of the Minecraft objective used to store <see cref="Pointer"/>.
	/// </summary>
	public static string AddressObjectiveCriterion => "dummy";

	/// <summary>
	/// The <see cref="Objective"/> used to store all <see cref="Pointer"/> values.
	/// </summary>
	public static Objective AddressObjective { get; } = new Objective(AddressObjectiveName, AddressObjectiveCriterion);

	/// <inheritdoc/>
	public IType Type { get; }

	/// <inheritdoc/>
	public string Identifier { get; }

	/// <summary>
	/// The number ID used to access the entity this <see cref="ObjectInstance"/> currently points to.
	/// </summary>
	public PrimitiveInstance.IntegerInstance Pointer { get; }


	/// <summary>
	/// Creates a new <see cref="ObjectInstance"/>.
	/// </summary>
	/// <param name="location">The location this <see cref="IInstance"/> will be created in.</param>
	/// <param name="type">The <see cref="IType"/> of this <see cref="IInstance"/>.</param>
	/// <param name="identifier">The <see cref="Identifier"/> of this <see cref="IInstance"/>.</param>
	/// <param name="pointer">The <see cref="Pointer"/> of this <see cref="ObjectInstance"/>.</param>
	public ObjectInstance(Compiler.CompileArguments location, IType type, string identifier, PrimitiveInstance.IntegerInstance pointer) {

		Type = type ?? throw new ArgumentNullException(nameof(type));
		Identifier = identifier;
		Pointer = pointer ?? throw new ArgumentNullException(nameof(pointer));

		location.Scope.AddInstance(this);

	}


	public void CreateEntity(Compiler.CompileArguments location) {

		const string tag = "mcs.new_object";
		FunctionWriter writer = location.Writer;

		// Write header comment.
		writer.WriteComments(
			$"Create entity to return from function.",
			indentBefore: true);

		// Create the entity. Use a tag to identify the entity.
		writer.WriteCommand($"execute at @p run summon minecraft:area_effect_cloud ~ ~ ~ {{Tags:[{tag}],Age:-1}}",
			$"Create a new entity for the new object of class type '{Type.Identifier}'.");

		// Get the entity's UUID.
		writer.WriteCommand($"execute store result score {MCSharpLinkerExtension.StorageSelector} {Pointer.Objective.Name} run data get entity @e[tag={tag},limit=1,sort=arbitrary] UUID[0] 1",
			$"Get the entity's UUID.");

		// Set the entity's address to its UUID.
		writer.WriteCommand($"scoreboard players operation @e[tag={tag},sort=arbitrary] {AddressObjective.Name} = {MCSharpLinkerExtension.StorageSelector} {Pointer.Objective.Name}",
			$"Assign the entity's address.");

		// Remove the temporary tag from the entity.
		writer.WriteCommand($"tag @e[tag={tag}] remove {tag}",
			$"Remove the temporary tag from the new entity.",
			indentAfter: true);

	}

	/// <summary>
	/// Use the <see cref="Pointer"/> to access the entity this <see cref="ObjectInstance"/> currently points to. Only one entity can be selected at a time.
	/// </summary>
	/// <param name="location">The location the entity will be accessed in.</param>
	/// <returns>The selector to the entity this <see cref="ObjectInstance"/> currently points to as a <see cref="string"/>.</returns>
	public virtual string GetSelector(Compiler.CompileArguments location) {

		// Get the writer.
		var writer = location.Writer;

		// Get the tag to use for selection.
		const string tag = "mcs.select";

		// Remove the tag from all other entities.
		writer.WriteCommand($"tag @e remove {tag}",
			$"Select an object entity with the address of a pointer.");

		// Tag the entity with the address of the pointer.
		writer.WriteCommand($"execute {GetExecuteAs()} run tag @s add {tag}");

		// Return the selector of entities with the tag (there should only be one).
		return $"@e[tag={tag},limit=1,sort=arbitrary]";

	}

	/// <summary>
	/// Use the <see cref="Pointer"/> to access the entity this <see cref="ObjectInstance"/> current points to.
	/// </summary>
	/// <returns>Returns an 'execute' command argument starting with "as", trimmed.</returns>
	public virtual string GetExecuteAs() {

		return $"as @e if score @s {AddressObjective.Name} = {MCSharpLinkerExtension.StorageSelector} {Pointer.Objective.Name}";

	}

	/// <inheritdoc/>
	public IInstance Copy(Compiler.CompileArguments location, string identifier) {

		var copy = new ObjectInstance(location, Type, identifier, Pointer);
		return copy;

	}

	/// <inheritdoc/>
	public void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
		(int offset, int length) = range.GetOffsetAndLength(block.Length);
		int expected = 1;
		if(length != expected) IInstance.GenerateInvalidBlockRangeException(length, expected);
		else location.Writer.WriteCommand($"scoreboard players operation {selector} {block[offset].Name} = {MCSharpLinkerExtension.StorageSelector} {Pointer.Objective.Name}",
			Identifier == null ? "Save anonymous object pointer to a block."
			: $"Save from object instance '{Identifier}' pointer to a block.");
	}

	/// <inheritdoc/>
	public void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
		(int offset, int length) = range.GetOffsetAndLength(block.Length);
		int expected = 1;
		if(length != expected) IInstance.GenerateInvalidBlockRangeException(length, expected);
		else location.Writer.WriteCommand($"scoreboard players operation {MCSharpLinkerExtension.StorageSelector} {Pointer.Objective.Name} = {selector} {block[offset].Name}",
			Identifier == null ? "Load anonymous object pointer from a block."
			: $"Load to object instance '{Identifier}' pointer from a block.");
	}

	public override string ToString() {
		return $"{Type.Identifier} {Identifier}";
	}

}
