using MCSharp.Compilation.Instancing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

	/// <summary>
	/// Represents a scope within code.
	/// </summary>
	public class Scope {

		/// <summary>
		/// The parent of this <see cref="Scope"/> (or <see langword="null"/> if root).
		/// </summary>
		public Scope Parent { get; } = null;

		private readonly ICollection<Scope> children = new LinkedList<Scope>();
		/// <summary>
		/// The read-only child <see cref="Scope"/>s of this <see cref="Scope"/>.
		/// <para>The only way to add more children is to call <see cref="Scope(Scope)"/>.</para>
		/// </summary>
		public IReadOnlyCollection<Scope> Children => (IReadOnlyCollection<Scope>)children;

		private readonly IDictionary<string, IInstance> instances = new Dictionary<string, IInstance>();
		/// <summary>
		/// The read-only <see cref="IInstance"/>s of this <see cref="Scope"/>.
		/// <para>The only way to add more <see cref="IInstance"/>s is to call <see cref="AddInstance"/>.</para>
		/// </summary>
		public IReadOnlyDictionary<string, IInstance> Instances => (IReadOnlyDictionary<string, IInstance>)instances;


		/// <summary>
		/// Creates a new <see cref="Scope"/> that is a child of <paramref name="parent"/>.
		/// </summary>
		/// <param name="parent">The parent of the new scope. Can be <see langword="null"/> to make this a root.</param>
		public Scope(Scope parent) {
			Parent = parent;
			Parent?.children.Add(this);
		}


		/// <summary>
		/// Adds the given <see cref="IInstance"/> to <see cref="Instances"/> if <paramref name="instance"/>'s <see cref="IInstance.Identifier"/>'s <see cref="string"/> does not exist as a key already.
		/// </summary>
		/// <param name="instance">The <see cref="IInstance"/> to add to this <see cref="Scope"/>'s <see cref="Instances"/>.</param>
		/// <returns>Returns a <see cref="ResultInfo"/> based on if there is already an <see cref="IInstance"/> with <paramref name="instance"/>'s <see cref="IInstance.Identifier"/>'s <see cref="string"/>.</returns>
		public ResultInfo AddInstance(IInstance instance) {
			string identifier = instance.Identifier.GetText();
			if(Instances.ContainsKey(identifier)) {
				return new ResultInfo(false, $"An instance with the name '{identifier}' already exists in this scope.");
			} else {
				instances.Add(identifier, instance);
				return ResultInfo.DefaultSuccess();
			}
		}

	}

}
