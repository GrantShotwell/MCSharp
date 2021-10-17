using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of a class type.
	/// </summary>
	public class ClassInstance : ObjectInstance {

		/// <summary>
		/// The <see cref="Objective"/>s used to store fields in the <see cref="ClassInstance"/> of this <see cref="IType"/>.
		/// </summary>
		public IReadOnlyDictionary<IField, Objective[]> FieldObjectives => AllFieldObjectives[Type];

		/// <summary>
		/// A collection of all <see cref="FieldObjectives"/> values.
		/// </summary>
		public static IDictionary<IType, IReadOnlyDictionary<IField, Objective[]>> AllFieldObjectives { get; } = new Dictionary<IType, IReadOnlyDictionary<IField, Objective[]>>();

		public ClassInstance(Compiler.CompileArguments location, IType type, string identifier, PrimitiveInstance.IntegerInstance objectId) : base(location, type, identifier, objectId) {

			#region Argument Checks
			if(type.ClassType != ClassType.Class)
				throw new IInstance.InvalidTypeException(type, "any class");
			#endregion

			//Type = type ?? throw new ArgumentNullException(nameof(type));
			//Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
			//ObjectId = objectId ?? throw new ArgumentNullException(nameof(objectId));
			
			if(!AllFieldObjectives.ContainsKey(type)) {

				var fieldObjectives = new Dictionary<IField, Objective[]>();
				AllFieldObjectives.Add(type, fieldObjectives);

				foreach(IMember member in type.Members) {

					switch(member.MemberType) {

						case MemberType.Field: {
							IType returnType = location.Compiler.DefinedTypes[member.ReturnTypeIdentifier];
							// TODO
							break;
						}

						case MemberType.Property: {
							// TODO?
							break;
						}

					}

				}

			}

			//location.Scope.AddInstance(this);

		}

		public static void ResetFieldObjectives() => AllFieldObjectives.Clear();

	}

}
