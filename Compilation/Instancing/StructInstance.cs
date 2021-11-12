using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of a struct type.
	/// </summary>
	[DebuggerDisplay("{ToString,nq}")]
	public class StructInstance : IInstance, IScopeHolder {

		/// <inheritdoc/>
		public IType Type { get; }

		/// <inheritdoc/>
		public string Identifier { get; }

		/// <summary>
		/// 
		/// </summary>
		public IReadOnlyDictionary<IField, IInstance> FieldInstances { get; }

		/// <inheritdoc/>
		public Scope Scope { get; }


		/// <summary>
		/// Creates a new <see cref="StructInstance"/>.
		/// </summary>
		/// <param name="location"></param>
		/// <param name="type"></param>
		/// <param name="identifier"></param>
		/// <exception cref="IInstance.InvalidTypeException"></exception>
		public StructInstance(Compiler.CompileArguments location, IType type, string identifier) {

			#region Argument Checks
			if(type.ClassType != ClassType.Struct)
				throw new IInstance.InvalidTypeException(type, "any struct");
			#endregion


			Type = type;
			Identifier = identifier;
			Scope = new Scope(null, type.Scope);
			Scope.Holder = this;
			IDictionary<IField, IInstance> fieldInstances = new Dictionary<IField, IInstance>(type.Members.Count);
			FieldInstances = (IReadOnlyDictionary<IField, IInstance>)fieldInstances;

			// Initialize members inside of type's scope.
			var typeLocation = new Compiler.CompileArguments(location.Compiler, location.Function, Scope, location.Predefined);
			foreach(IMember member in type.Members) {

				switch(member.MemberType) {

					case MemberType.Field: {
						var field = member.Definition as IField;
						typeLocation.Compiler.CompileExpression(typeLocation, field.Initializer.Context, out IInstance value);
						fieldInstances.Add(field, value.Copy(typeLocation, member.Identifier));
						break;
					}

					case MemberType.Property: {
						// TODO?
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

		/// <inheritdoc/>
		public void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(_, int length) = range.GetOffsetAndLength(block.Length);
			int expected = Type.GetBlockSize(location.Compiler);
			if(length != expected) IInstance.GenerateInvalidBlockRangeException(length, expected);
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range) {
			(_, int length) = range.GetOffsetAndLength(block.Length);
			int expected = Type.GetBlockSize(location.Compiler);
			if(length != expected) IInstance.GenerateInvalidBlockRangeException(length, expected);
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{Type.Identifier} {Identifier}";
		}

	}

}
