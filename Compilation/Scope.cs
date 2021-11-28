using MCSharp.Compilation.Instancing;
using MCSharp.Linkage;
using System;
using System.Collections.Generic;

namespace MCSharp.Compilation;

/// <summary>
/// Represents a scope within code.
/// </summary>
public class Scope {

	/// <summary>
	/// The identifier of this <see cref="Scope"/>. Anonymous scope have a <see langword="null"/> <see cref="Name"/>.
	/// </summary>
	public string Name { get; }

	private IScopeHolder holder = null;
	/// <summary>
	/// The <see cref="IScopeHolder"/> that holds this <see cref="Scope"/>, if any.
	/// </summary>
	public IScopeHolder Holder {
		get => holder ?? Parent?.Holder;
		set => holder = value;
	}

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
	/// <para>The only way to add more children is to call <see cref="Scope(string, Scope)"/>.</para>
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
	/// <exception cref="InvalidOperationException">Thrown when <paramref name="parent"/> already contains an immediate child called <paramref name="name"/>.</exception>
	public Scope(string name, Scope parent) {

		Name = name;

		Parent = parent;
		Parent?.children.Add(this);

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
			current = current.Parent;
		}

		return null;

	}

	/// <summary>
	/// Finds the closest parent to this <see cref="Scope"/> with the given <see cref="Name"/>.
	/// </summary>
	/// <param name="name">The <see cref="Name"/> to check for.</param>
	/// <returns>Returns the <see cref="Scope"/> found.</returns>
	public Scope FindFirstParentScopeByName(string name) {

		Scope current = Parent;
		while(current != null) {
			if(current.Name == name) return current;
			current = current.Parent;
		}

		return null;

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
	/// Finds the closest parent to this <see cref="Scope"/> with a name.
	/// </summary>
	/// <returns>Returns the <see cref="Scope"/> found.</returns>
	public Scope GetFirstNamedParent() {

		// Iterate upwards until a named parent is found.
		Scope current = Parent;
		while(current != null) {
			if(current.Name != null) return current;
			current = current.Parent;
		}

		// No named parent found.
		return null;

	}

	/// <summary>
	/// Finds the closest parent to this <see cref="Scope"/> with a name, or <see langword="this"/>.
	/// </summary>
	/// <returns>Returns the <see cref="Scope"/> found.</returns>
	public Scope GetFirstNamedParentOrThis() {

		if(Name != null) return this;
		else return GetFirstNamedParent();

	}

	/// <summary>
	/// Finds the closest <see cref="IScopeHolder"/> to this <see cref="Scope"/> that is an <see cref="IInstance"/> or <see cref="IType"/>.
	/// </summary>
	/// <returns>Returns the <see cref="IScopeHolder"/> found. Will always be one of the two interfaces or <see	langword="null"/>.</returns>
	public IScopeHolder GetInstanceOrTypeHolder() {

		// Iterate upwards until an instance or type holder is found.
		Scope current = this;
		while(current != null) {
			if(current.holder is IInstance) return current.holder;
			else if(current.holder is IType) return current.holder;
			current = current.Parent;
		}

		// No holder found.
		return null;

	}

}
