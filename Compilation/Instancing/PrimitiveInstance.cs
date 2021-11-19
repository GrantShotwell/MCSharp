using MCSharp.Linkage;
using MCSharp.Linkage.Extensions;
using MCSharp.Linkage.Minecraft;
using MCSharp.Linkage.Minecraft.Text;
using System;
using System.Diagnostics;

namespace MCSharp.Compilation.Instancing;

/// <summary>
/// Represents an instance of a primitive type.
/// </summary>
[DebuggerDisplay("{Type.Identifier,nq} {Identifier,nq}")]
public abstract class PrimitiveInstance : IInstance {

	/// <inheritdoc/>
	public IType Type { get; }

	/// <inheritdoc/>
	public string Identifier { get; }


	// Private to disallow extensions to make primitive types because that is not supported.
	/// <summary>
	/// Base constructor for all <see cref="PrimitiveInstance"/>s.
	/// </summary>
	/// <param name="type">The <see cref="IType"/> that defines this instance.</param>
	/// <param name="identifier">The local identifier for this instance.</param>
	private PrimitiveInstance(IType type, string identifier) {

		if(type.ClassType != ClassType.Primitive)
			throw new IInstance.InvalidTypeException(type, "a specific primitive");
		Type = type;

		Identifier = identifier;

	}

	/// <inheritdoc/>
	public abstract IInstance Copy(Compiler.CompileArguments location, string identifier);

	/// <inheritdoc/>
	public abstract void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range);

	/// <inheritdoc/>
	public abstract void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range);


	/// <summary>
	/// Represents an <see cref="IConstantInstance"/> of an objective.
	/// </summary>
	[DebuggerDisplay("const {Type.Identifier,nq} {Identifier,nq} = {Value,nq}")]
	public class ObjectiveInstance : PrimitiveInstance, IConstantInstance<Objective> {

		/// <summary>
		/// The <see cref="Objective"/> that this <see cref="ObjectiveInstance"/> represents.
		/// </summary>
		public Objective Value { get; }
		/// <inheritdoc/>
		object IConstantInstance.Value => Value;


		/// <summary>
		/// Creates a new <see cref="ObjectiveInstance"/> that holds <paramref name="value"/>.
		/// </summary>
		/// <param name="type">The <see cref="IType"/> that defines this instance.</param>
		/// <param name="identifier">The local identifier for this instance.</param>
		/// <param name="value">The value to hold within this <see cref="IConstantInstance"/>.</param>
		public ObjectiveInstance(IType type, string identifier, Objective value) : base(type, identifier) {
			Value = value ?? throw new ArgumentNullException(nameof(value));
		}


		/// <inheritdoc/>
		public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(_, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 1) throw IInstance.GenerateInvalidBlockRangeException(length, 1);
			else throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(_, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 1) throw IInstance.GenerateInvalidBlockRangeException(length, 1);
			else throw new NotImplementedException();
		}

	}

	/// <summary>
	/// Represents an <see cref="IInstance"/> of an integer value.
	/// </summary>
	public class IntegerInstance : PrimitiveInstance {

		/// <summary>
		/// The <see cref="Linkage.Minecraft.Objective"/> that holds the value of this <see cref="IntegerInstance"/>.
		/// </summary>
		public Objective Objective { get; }


		public IntegerInstance(IType type, string identifier, Objective objective) : base(type, identifier) {
			Objective = objective;
		}


		/// <inheritdoc/>
		public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(int offset, int length) = range.GetOffsetAndLength(block.Length);
			int expected = 1;
			if(length != expected) throw IInstance.GenerateInvalidBlockRangeException(length, expected);
			else location.Writer.WriteCommand($"scoreboard players operation {selector} {block[offset].Name} = {MCSharpLinkerExtension.StorageSelector} {Objective.Name}",
				"Save an integer value to a block.");
		}

		/// <inheritdoc/>
		public override void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(int offset, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 1) throw IInstance.GenerateInvalidBlockRangeException(length, 1);
			else location.Writer.WriteCommand($"scoreboard players operation {MCSharpLinkerExtension.StorageSelector} {Objective.Name} = {selector} {block[offset].Name}",
				"Load an integer value from a block.");
		}


		/// <summary>
		/// Represents the <see cref="IConstantInstance"/> version of <see cref="IntegerInstance"/>.
		/// </summary>
		[DebuggerDisplay("const {Type.Identifier,nq} {Identifier,nq} = {Value,nq}")]
		public class Constant : PrimitiveInstance, IConstantInstance<int> {

			/// <summary>
			/// The value of this <see cref="Constant"/>.
			/// </summary>
			public int Value { get; }
			/// <inheritdoc/>
			object IConstantInstance.Value => Value;


			/// <summary>
			/// Creates a new <see cref="Constant"/> that holds <paramref name="value"/>.
			/// </summary>
			/// <param name="type">The <see cref="IType"/> that defines this instance.</param>
			/// <param name="identifier">The local identifier for this instance.</param>
			/// <param name="value">The value to hold within this <see cref="IConstantInstance"/>.</param>
			public Constant(IType type, string identifier, int value) : base(type, identifier) {
				Value = value;
			}


			/// <inheritdoc/>
			public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
				Objective objective = Objective.AddObjective(location.Writer, null, "dummy");
				IntegerInstance instance = new IntegerInstance(Type, identifier, objective);
				location.Writer.WriteCommand($"scoreboard players set {MCSharpLinkerExtension.StorageSelector} {instance.Objective.Name} {Value}",
					"Copy a constant integer value.");
				return instance;
			}

			/// <inheritdoc/>
			public override void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
				(int offset, int length) = range.GetOffsetAndLength(block.Length);
				int expected = 1;
				if(length != expected) throw IInstance.GenerateInvalidBlockRangeException(length, expected);
				else location.Writer.WriteCommand($"scoreboard players set {selector} {block[offset].Name} {Value}",
					"Save a constant integer value to a block.");
			}

			/// <inheritdoc/>
			public override void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
				throw new InvalidOperationException("Cannot load a constant integer value from a block.");
			}

		}

	}

	/// <summary>
	/// Represents an <see cref="IInstance"/> of a boolean value.
	/// </summary>
	public class BooleanInstance : PrimitiveInstance {

		/// <summary>
		/// The <see cref="Linkage.Minecraft.Objective"/> that holds the value of this <see cref="BooleanInstance"/>.
		/// </summary>
		public Objective Objective { get; }


		public BooleanInstance(IType type, string identifier, Objective objective) : base(type, identifier) {
			Objective = objective;
		}


		/// <inheritdoc/>
		public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(int offset, int length) = range.GetOffsetAndLength(block.Length);
			int expected = 1;
			if(length != expected) throw IInstance.GenerateInvalidBlockRangeException(length, expected);
			else location.Writer.WriteCommand($"scoreboard players operation {selector} {block[offset].Name} = {MCSharpLinkerExtension.StorageSelector} {Objective.Name}",
				"Save a boolean value to a block.");
		}

		/// <inheritdoc/>
		public override void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(int offset, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 1) throw IInstance.GenerateInvalidBlockRangeException(length, 1);
			else location.Writer.WriteCommand($"scoreboard players operation {MCSharpLinkerExtension.StorageSelector} {Objective.Name} = {selector} {block[offset].Name}",
				"Load a boolean value from a block.");
		}


		/// <summary>
		/// Represents the <see cref="IConstantInstance"/> version of <see cref="BooleanInstance"/>.
		/// </summary>
		[DebuggerDisplay("const {Type.Identifier,nq} {Identifier,nq} = {Value,nq}")]
		public class Constant : PrimitiveInstance, IConstantInstance<bool> {

			/// <summary>
			/// The value of this <see cref="Constant"/>.
			/// </summary>
			public bool Value { get; }
			/// <inheritdoc/>
			object IConstantInstance.Value => Value;


			/// <summary>
			/// Creates a new <see cref="Constant"/> that holds <paramref name="value"/>.
			/// </summary>
			/// <param name="type">The <see cref="IType"/> that defines this instance.</param>
			/// <param name="identifier">The local identifier for this instance.</param>
			/// <param name="value">The value to hold within this <see cref="IConstantInstance"/>.</param>
			public Constant(IType type, string identifier, bool value) : base(type, identifier) {
				Value = value;
			}


			/// <inheritdoc/>
			public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
				throw new NotImplementedException();
			}

			/// <inheritdoc/>
			public override void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
				(int offset, int length) = range.GetOffsetAndLength(block.Length);
				int expected = 1;
				if(length != expected) throw IInstance.GenerateInvalidBlockRangeException(length, expected);
				else location.Writer.WriteCommand($"scoreboard players set {selector} {block[offset].Name} {(Value ? 1 : 0)}",
					"Save a constant boolean value to a block.");
			}

			/// <inheritdoc/>
			public override void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
				throw new InvalidOperationException("Cannot load a constant value from a block.");
			}

		}

	}

	[DebuggerDisplay("const {Type.Identifier,nq} {Identifier,nq} = \"{Value,nq}\"")]
	public class StringInstance : PrimitiveInstance, IConstantInstance<string> {

		/// <inheritdoc/>
		public string Value { get; set; }
		/// <inheritdoc/>
		object IConstantInstance.Value => Value;


		/// <summary>
		/// Creates a new <see cref="StringInstance"/> that holds <paramref name="value"/>.
		/// </summary>
		/// <param name="type">The <see cref="IType"/> that defines this instance.</param>
		/// <param name="identifier">The local identifier for this instance.</param>
		/// <param name="value">The value to hold within this <see cref="IConstantInstance"/>.</param>
		public StringInstance(IType type, string identifier, string value) : base(type, identifier) {
			Value = value;
		}


		/// <inheritdoc/>
		public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(int _, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 0) throw IInstance.GenerateInvalidBlockRangeException(length, 0);
			else return;
		}

		/// <inheritdoc/>
		public override void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(_, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 1) throw IInstance.GenerateInvalidBlockRangeException(length, 1);
			else return;
		}

	}

	[DebuggerDisplay("const {Type.Identifier,nq} {Identifier,nq} = \"{Value,nq}\"")]
	public class SelectorInstance : PrimitiveInstance, IConstantInstance<Selector> {

		/// <inheritdoc/>
		public Selector Value { get; set; }
		/// <inheritdoc/>
		object IConstantInstance.Value => Value;


		/// <summary>
		/// Creates a new <see cref="SelectorInstance"/> that holds <paramref name="value"/>.
		/// </summary>
		/// <param name="type">The <see cref="IType"/> that defines this instance.</param>
		/// <param name="identifier">The local identifier for this instance.</param>
		/// <param name="value">The value to hold within this <see cref="IConstantInstance"/>.</param>
		public SelectorInstance(IType type, string identifier, Selector value) : base(type, identifier) {
			Value = value;
		}


		/// <inheritdoc/>
		public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(int _, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 0) throw IInstance.GenerateInvalidBlockRangeException(length, 0);
			else return;
		}

		/// <inheritdoc/>
		public override void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(_, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 1) throw IInstance.GenerateInvalidBlockRangeException(length, 1);
			else return;
		}

	}

	[DebuggerDisplay("const {Type.Identifier,nq} {Identifier,nq} = {Value,nq}")]
	public class JsonInstance : PrimitiveInstance, IConstantInstance<RawText> {

		/// <inheritdoc/>
		public RawText Value { get; set; }
		/// <inheritdoc/>
		object IConstantInstance.Value => Value;

		/// <summary>
		/// Creates a new <see cref="JsonInstance"/> that holds <paramref name="value"/>.
		/// </summary>
		/// <param name="type">The <see cref="IType"/> that defines this instance.</param>
		/// <param name="identifier">The local identifier for this instance.</param>
		/// <param name="value">The value to hold within this <see cref="IConstantInstance"/>.</param>
		public JsonInstance(IType type, string identifier, RawText value) : base(type, identifier) {
			Value = value;
		}

		/// <inheritdoc/>
		public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(int _, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 0) throw IInstance.GenerateInvalidBlockRangeException(length, 0);
			else return;
		}

		/// <inheritdoc/>
		public override void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(_, int length) = range.GetOffsetAndLength(block.Length);
			if(length != 1) throw IInstance.GenerateInvalidBlockRangeException(length, 1);
			else return;
		}

	}

}
