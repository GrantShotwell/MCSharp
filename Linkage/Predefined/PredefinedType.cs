using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Antlr4.Runtime.Tree;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Minecraft;
using MCSharp.Compilation;
using MCSharp.Collections;

namespace MCSharp.Linkage.Predefined {

	/// <summary>
	/// Represents a predefined type.
	/// </summary>
	public class PredefinedType : IType {

		/// <inheritdoc/>
		public Modifier Modifiers { get; }

		/// <inheritdoc/>
		public ClassType ClassType { get; }

		/// <inheritdoc/>
		public string Identifier { get; }

		/// <inheritdoc/>
		public ICollection<IType> Inheritance { get; } = new List<IType>();

		/// <inheritdoc/>
		public Scope Scope { get; set; }

		/// <summary>
		/// The members defined by this type definition.
		/// </summary>
		public IReadOnlyCollection<PredefinedMember> Members { get; }
		/// <inheritdoc/>
		IReadOnlyCollection<IMember> IType.Members => Members;

		/// <summary>
		/// The constructors defined by this type definition.
		/// </summary>
		public IReadOnlyCollection<PredefinedConstructor> Constructors { get; }
		/// <inheritdoc/>
		IReadOnlyCollection<IConstructor> IType.Constructors => Constructors;

		/// <summary>
		/// The type definitions defined within this type definition.
		/// </summary>
		public IReadOnlyCollection<PredefinedType> SubTypes { get; }
		/// <inheritdoc/>
		IReadOnlyCollection<IType> IType.SubTypes => SubTypes;

		private InitializeInstanceDelegate Init { get; }

		/// <inheritdoc/>
		public IHashSetDictionary<Operation, IOperation> Operations { get; }


		/// <summary>
		/// Creates a new predefined type definition.
		/// </summary>
		/// <param name="modifiers">The modifiers that affect this type definition.</param>
		/// <param name="classType">Whether this type is a class or a struct.</param>
		/// <param name="identifier">The local identifier for this type definition.</param>
		/// <param name="members">The members defined by this type definition.</param>
		/// <param name="subTypes">The type definitions defined by this type definition.</param>
		public PredefinedType(Modifier modifiers, ClassType classType, string identifier, PredefinedMember[] members,
		PredefinedConstructor[] constructors, PredefinedType[] subTypes, InitializeInstanceDelegate init, IHashSetDictionary<Operation, IOperation> operations) {
			Modifiers = modifiers;
			ClassType = classType;
			Identifier = identifier;
			Members = members;
			Constructors = constructors;
			SubTypes = subTypes;
			Init = init;
			Operations = operations;
		}


		public delegate IInstance InitializeInstanceDelegate(Compiler.CompileArguments location, string identifier);

		/// <inheritdoc/>
		public IInstance InitializeInstance(Compiler.CompileArguments location, string identifier) {

			IInstance result = Init(location, identifier);
			location.Scope.AddInstance(result);
			return result;
			
		}

		public void Dispose() {
			foreach(PredefinedMember member in Members) member.Dispose();
		}

	}

}
