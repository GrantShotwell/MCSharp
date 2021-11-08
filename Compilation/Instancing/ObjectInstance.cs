﻿using MCSharp.Linkage;
using MCSharp.Linkage.Extensions;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of the base object class, similar to an instance of C#'s <see cref="object"/>.
	/// </summary>
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


		/// <summary>
		/// Use the <see cref="Pointer"/> to access the entity this <see cref="ObjectInstance"/> currently points to. Only one entity can be selected at a time.
		/// </summary>
		/// <param name="location">The location the entity will be accessed in.</param>
		/// <returns>The selector to the entity this <see cref="ObjectInstance"/> currently points to as a <see cref="string"/>.</returns>
		public string GetSelector(Compiler.CompileArguments location) {
			
			// Get the writer.
			var writer = location.Writer;

			// Get the tag to use for selection.
			const string tag = "mcs.select";

			// Remove the tag from all other entities.
			writer.WriteCommand($"tag @e remove {tag}",
				$"Select an object entity with the address of a pointer.");
			
			// Tag the entity with the address of the pointer.
			writer.WriteCommand($"execute as @e if score @s {AddressObjective.Name} = {MCSharpLinkerExtension.StorageSelector} {Pointer.Objective.Name} " +
			                    $"run tag @s add {tag}");
			
			// Return the selector of entities with the tag (there should only be one).
			return $"@e[tag={tag},limit=1,sort=arbitrary]";

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
				: $"Save object instance '{Identifier}' pointer to a block.");
		}

	}

}
