﻿using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of a primitive type.
	/// </summary>
	public abstract class PrimitiveInstance : IInstance {

		/// <inheritdoc/>
		public IType Type { get; }
		
		/// <inheritdoc/>
		public ITerminalNode Identifier { get; }


		// Private to disallow extensions to make primitive types.
		private PrimitiveInstance(IType type, ITerminalNode identifier) {

			if(type.ClassType != ClassType.Primitive)
				throw new IInstance.InvalidTypeException(type, "a specific primitive");
			Type = type;

			Identifier = identifier;

		}


		/// <summary>
		/// Represents an <see cref="IConstantInstance"/> of an objective.
		/// </summary>
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
			/// <param name="type"></param>
			/// <param name="identifier"></param>
			/// <param name="value"></param>
			public ObjectiveInstance(IType type, ITerminalNode identifier, Objective value) : base(type, identifier) {
				Value = value ?? throw new ArgumentNullException(nameof(value));
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


			public IntegerInstance(IType type, ITerminalNode identifier, Objective objective) : base(type, identifier) {
				Objective = objective;
			}


			/// <summary>
			/// Represents the <see cref="IConstantInstance"/> version of <see cref="IntegerInstance"/>.
			/// </summary>
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
				/// <param name="type"></param>
				/// <param name="identifier"></param>
				/// <param name="value"></param>
				public Constant(IType type, ITerminalNode identifier, int value) : base(type, identifier) {
					Value = value;
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


			public BooleanInstance(IType type, ITerminalNode identifier, Objective objective) : base(type, identifier) {
				Objective = objective;
			}


			/// <summary>
			/// Represents the <see cref="IConstantInstance"/> version of <see cref="BooleanInstance"/>.
			/// </summary>
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
				/// <param name="type"></param>
				/// <param name="identifier"></param>
				/// <param name="value"></param>
				public Constant(IType type, ITerminalNode identifier, bool value) : base(type, identifier) {
					Value = value;
				}

			}

		}

	}

}
