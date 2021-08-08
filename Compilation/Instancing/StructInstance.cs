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

		public IReadOnlyDictionary<IField, IInstance> FieldInstances { get; }

		public Scope Scope { get; set; }

		public StructInstance(Compiler.CompileArguments location, IType type, string identifier) {

			#region Argument Checks
			if(type.ClassType != ClassType.Struct)
				throw new IInstance.InvalidTypeException(type, "any struct");
			#endregion

			Type = type;
			Identifier = identifier;
			Scope = new Scope(null, type.Scope, this);
			IDictionary<IField, IInstance> fieldInstances = new Dictionary<IField, IInstance>(type.Members.Count);
			FieldInstances = (IReadOnlyDictionary<IField, IInstance>)fieldInstances;

			var compile = new Compiler.CompileArguments(location.Compiler, location.Function, Scope, location.Predefined);
			foreach(IMember member in type.Members) {

				switch(member.MemberType) {

					case MemberType.Field: {
						var field = member.Definition as IField;
						compile.Compiler.CompileExpression(compile, field.Initializer.Context, out IInstance value);
						fieldInstances.Add(field, value.Copy(compile, member.Identifier));
						break;
					}

					case MemberType.Property: {
						// TODO
						break;
					}

				}

			}

			location.Scope.AddInstance(this);

		}


		/// <inheritdoc/>
		public IInstance Copy(Compiler.CompileArguments location, string identifier) {
			throw new NotImplementedException();
		}

	}

}
