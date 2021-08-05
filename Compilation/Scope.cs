using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
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
		/// The identifier of this <see cref="Scope"/>. Anonymous scope have a <see langword="null"/> <see cref="Name"/>.
		/// </summary>
		public string Name { get; }

		private readonly IScopeHolder holder = null;
		/// <summary>
		/// The <see cref="IScopeHolder"/> that holds this <see cref="Scope"/>, if any.
		/// </summary>
		public IScopeHolder Holder => holder ?? Parent?.Holder;

		/// <summary>
		/// The root of this <see cref="Scope"/>, found recursively.
		/// </summary>
		public Scope Root => Parent == null ? this : Parent?.Root ?? Parent;

		/// <summary>
		/// The parent of this <see cref="Scope"/> (or <see langword="null"/> if root).
		/// </summary>
		public Scope Parent { get; } = null;

		private readonly ICollection<Scope> children = new List<Scope>();
		/// <summary>
		/// The read-only child <see cref="Scope"/>s of this <see cref="Scope"/>.
		/// <para>The only way to add more children is to call <see cref="Scope(Scope)"/>.</para>
		/// </summary>
		public IReadOnlyCollection<Scope> Children => (IReadOnlyCollection<Scope>)children;

		private readonly IDictionary<string, IInstance> instances = new Dictionary<string, IInstance>();
		/// <summary>
		/// The read-only <see cref="IInstance"/>s of this <see cref="Scope"/>.
		/// </summary>
		/// <seealso cref="AddInstance(IInstance)"/>
		public IReadOnlyDictionary<string, IInstance> Instances => (IReadOnlyDictionary<string, IInstance>)instances;

		private readonly ICollection<IInstance> anonymousInstances = new LinkedList<IInstance>();
		/// <summary>
		/// The read-only <see cref="IInstance"/>s with no name of this <see cref="Scope"/>.
		/// </summary>
		/// <seealso cref="AddInstance(IInstance)"/>
		public IReadOnlyCollection<IInstance> AnonymousInstances => (IReadOnlyCollection<IInstance>)anonymousInstances;


		/// <summary>
		/// Creates a new <see cref="Scope"/> that is a child of <paramref name="parent"/>.
		/// </summary>
		/// <param name="name">The name of this <see cref="Scope"/>. Can be <see langword="null"/> to make this anonymous.</param>
		/// <param name="parent">The parent of the new <see cref="Scope"/>. Can be <see langword="null"/> to make this a root.</param>
		/// <param name="holder">The <see cref="IScopeHolder"/> to hold this <see cref="Scope"/>.</param>
		/// <exception cref="InvalidOperationException">Thrown when <paramref name="parent"/> already contains an immediate child called <paramref name="name"/>.</exception>
		public Scope(string name, Scope parent, IScopeHolder holder) {

			Name = name;

			Parent = parent;
			if(Parent != null) {
				if(name != null) {
					foreach(Scope child in Parent.Children)
						if(child.Name == Name) throw new InvalidOperationException($"'{nameof(parent)}' already contains a child called '{Name}'.");
				}
				Parent.children.Add(this);
			}

			this.holder = holder;
			if(holder != null) holder.Scope = this;

		}


		/// <summary>
		/// Adds the given <see cref="IInstance"/> to <see cref="Instances"/> if <paramref name="instance"/>'s <see cref="IInstance.Identifier"/>'s <see cref="string"/> does not exist as a key already.
		/// </summary>
		/// <param name="instance">The <see cref="IInstance"/> to add to this <see cref="Scope"/>'s <see cref="Instances"/>.</param>
		/// <returns>Returns a <see cref="ResultInfo"/> based on if there is already an <see cref="IInstance"/> with <paramref name="instance"/>'s <see cref="IInstance.Identifier"/>'s <see cref="string"/>.</returns>
		public ResultInfo AddInstance(IInstance instance) {

			string identifier = instance.Identifier;
			if(identifier == null) {
				anonymousInstances.Add(instance);
				return ResultInfo.DefaultSuccess;
			} else if(Instances.ContainsKey(identifier)) {
				return new ResultInfo(false, $"An instance with the name '{identifier}' already exists in this scope.");
			} else {
				instances.Add(identifier, instance);
				return ResultInfo.DefaultSuccess;
			}

		}

		/// <summary>
		/// Starting with <see langword="this"/>, goes through parents' <see cref="Instances"/> to find the <see cref="IInstance"/> from <paramref name="identifier"/>.
		/// </summary>
		/// <param name="identifier">The key in <see cref="Instances"/> to look for.</param>
		/// <returns>Returns the <see cref="IInstance"/> found or <see langword="null"/> when not found.</returns>
		public IInstance FindFirstInstanceByName(string identifier) {

			Scope current = this;
			while(current != null) {
				IReadOnlyDictionary<string, IInstance> instances = current.Instances;
				if(instances.ContainsKey(identifier)) return instances[identifier];
				else current = current.Parent;
			}

			return null;

		}

		/// <summary>
		/// Finds the closest parent to this <see cref="Scope"/> with the given <see cref="Name"/>.
		/// </summary>
		/// <param name="name">The <see cref="Name"/> to check for.</param>
		/// <returns>Returns the <see cref="Scope"/> found.</returns>
		public Scope FindFirstParentScopeByName(string name) {

			if(Parent.Name == name) return Parent;
			else return Parent.FindFirstParentScopeByName(name);

		}

		/// <summary>
		/// Finds the farthest parent to this <see cref="Scope"/> with the given <see cref="Name"/>.
		/// </summary>
		/// <param name="name">The <see cref="Name"/> to check for.</param>
		/// <returns>Returns the <see cref="Scope"/> found.</returns>
		public Scope FindLastParentScopeByName(string name) {

			Scope match = null;

			Scope current = Parent;
			while(current != null) {
				if(current.Name == name) match = current;
				current = current.Parent;
			}

			return match;

		}

		/// <summary>
		/// Finds the closest parent to this <see cref="Scope"/>.
		/// </summary>
		/// <returns>Returns the <see cref="Scope"/> found.</returns>
		public Scope GetFirstNamedParent() {

			Scope current = Parent;
			while(current != null) {
				if(current.Name != null) return current;
				else current = current.Parent;
			}

			return null;

		}

	}

}
