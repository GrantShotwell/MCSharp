using MCSharp.Collections;
using MCSharp.Compilation.Instancing;
using System;
using System.Collections.Generic;

namespace MCSharp.Linkage {

	/// <summary>
	/// Represents a type definition.
	/// </summary>
	public interface IType : IDisposable {

		/// <summary>
		/// The modifiers that affect this type definition.
		/// </summary>
		public Modifier Modifiers { get; }

		/// <summary>
		/// Whether this type definition is for a class or a struct.
		/// </summary>
		public ClassType ClassType { get; }

		/// <summary>
		/// The local identifier for this type definition.
		/// </summary>
		public string Identifier { get; }

		/// <summary>
		/// The members defined by this type definition.
		/// </summary>
		public IReadOnlyCollection<IMember> Members { get; }

		/// <summary>
		/// The constructors defined by this type definition.
		/// </summary>
		public IReadOnlyCollection<IConstructor> Constructors { get; }

		/// <summary>
		/// Type definitions defined within this type definition.
		/// </summary>
		public IReadOnlyCollection<IType> SubTypes { get; }

		/// <summary>
		/// 
		/// </summary>
		public IHashSetDictionary<Operation, IOperation> Operations { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="scope"></param>
		/// <param name="identifier"></param>
		/// <returns></returns>
		public IInstance InitializeInstance(Compilation.FunctionWriter writer, Compilation.Scope scope, Antlr4.Runtime.Tree.ITerminalNode identifier);

	}

}
