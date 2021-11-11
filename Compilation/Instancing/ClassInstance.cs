using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of a class type.
	/// </summary>
	[DebuggerDisplay("{ToString,nq}")]
	public class ClassInstance : ObjectInstance {

		/// <summary>
		/// The <see cref="Objective"/>s used to store fields in the <see cref="ClassInstance"/> of this <see cref="IType"/>.
		/// </summary>
		public IReadOnlyDictionary<IField, Objective[]> FieldObjectives { get; }

		public ClassInstance(Compiler.CompileArguments location, IType type, string identifier, PrimitiveInstance.IntegerInstance objectId, IReadOnlyDictionary<IField, Objective[]> fieldObjectives)
		: base(location, type, identifier, objectId) {

			#region Argument Checks
			if(type.ClassType != ClassType.Class)
				throw new IInstance.InvalidTypeException(type, "any class");
			#endregion

			// Handled by base constructor.
			//Type = type ?? throw new ArgumentNullException(nameof(type));
			//Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
			//ObjectId = objectId ?? throw new ArgumentNullException(nameof(objectId));

			FieldObjectives = fieldObjectives ?? throw new ArgumentNullException(nameof(fieldObjectives));

			// Handled by base constructor.
			//location.Scope.AddInstance(this);

		}
	
		/// <inheritdoc/>
		public override string ToString() {
			return $"{Type.Identifier} {Identifier}";
		}

	}

}
