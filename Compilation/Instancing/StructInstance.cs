using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of a struct type.
	/// </summary>
	public class StructInstance : IInstance, IScopeHolder {

		/// <inheritdoc/>
		public IType Type { get; }

		/// <inheritdoc/>
		public string Identifier { get; }

		public IReadOnlyList<IInstance> FieldInstances { get; }

		public Scope Scope { get; set; }

		public StructInstance(Compiler.CompileArguments location, IType type, string identifier) {

			#region Argument Checks
			if(type.ClassType != ClassType.Struct)
				throw new IInstance.InvalidTypeException(type, "any struct");
			#endregion

			Type = type;
			Identifier = identifier;
			Scope = new Scope(null, type.Scope, this);
			IList<IInstance> fieldInstances = new List<IInstance>(type.Members.Count);
			FieldInstances = (IReadOnlyList<IInstance>)fieldInstances;

			var compile = new Compiler.CompileArguments(location.Compiler, location.Function, Scope, location.Predefined);
			foreach(IMember member in type.Members) {

				switch(member.MemberType) {

					case MemberType.Field: {
						var field = member.Definition as IField;
						compile.Compiler.CompileExpression(compile, field.Initializer.Context, out IInstance value);
						value = value.Copy(compile, member.Identifier);
						fieldInstances.Add(value);
						break;
					}

					case MemberType.Property: {
						// TODO
						break;
					}

				}

			}

		}


		/// <inheritdoc/>
		public IInstance Copy(Compiler.CompileArguments compile, string identifier) {
			throw new NotImplementedException();
		}

	}

}
