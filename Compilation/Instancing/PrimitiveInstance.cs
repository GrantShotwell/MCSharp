using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using MCSharp.Linkage.Extensions;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MCSharp.Compilation.Instancing {

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
		/// 
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
			/// <param name="value"></param>
			public ObjectiveInstance(IType type, string identifier, Objective value) : base(type, identifier) {
				Value = value ?? throw new ArgumentNullException(nameof(value));
			}


			/// <inheritdoc/>
			public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
				throw new NotImplementedException();
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
				/// <param name="value"></param>
				public Constant(IType type, string identifier, int value) : base(type, identifier) {
					Value = value;
				}


				/// <inheritdoc/>
				public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
					Objective objective = Objective.AddObjective(location.Writer, null, "dummy");
					IntegerInstance instance = new IntegerInstance(Type, identifier, objective);
					location.Writer.WriteCommand($"scoreboard players set {MCSharpLinkerExtension.StorageSelector} {instance.Objective.Name} {Value}");
					return instance;
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
				/// <param name="value"></param>
				public Constant(IType type, string identifier, bool value) : base(type, identifier) {
					Value = value;
				}


				/// <inheritdoc/>
				public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
					throw new NotImplementedException();
				}

			}

		}

		[DebuggerDisplay("const {Type.Identifier,nq} {Identifier,nq} = {Value,nq}")]
		public class StringInstance : PrimitiveInstance, IConstantInstance<string> {

			/// <inheritdoc/>
			public string Value { get; }
			/// <inheritdoc/>
			object IConstantInstance.Value => Value;


			/// <summary>
			/// Creates a new <see cref="StringInstance"/> that holds <paramref name="value"/>.
			/// </summary>
			/// <param name="type">The <see cref="IType"/> that defines this instance.</param>
			/// <param name="identifier">The local identifier for this instance.</param>
			/// <param name="value"></param>
			public StringInstance(IType type, string identifier, string value) : base(type, identifier) {
				Value = value;
			}


			/// <inheritdoc/>
			public override IInstance Copy(Compiler.CompileArguments location, string identifier) {
				throw new NotImplementedException();
			}

		}

	}

}
