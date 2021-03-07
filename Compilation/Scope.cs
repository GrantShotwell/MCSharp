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

		/// <summary>
		/// The root of this <see cref="Scope"/>.
		/// </summary>
		/// <exception cref="StackOverflowException">Thrown when the tree is (somehow) circular.</exception>
		public Scope Root => Parent?.Root ?? this;

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
		/// <param name="parent">The parent of the new scope. Can be <see langword="null"/> to make this a root.</param>
		/// <exception cref="InvalidOperationException">Thrown when <paramref name="parent"/> already contains a child called </exception>
		public Scope(string name, Scope parent) {

			Name = name;

			Parent = parent;
			if(Parent != null) {
				foreach(Scope child in Parent.Children)
					if(child.Name == Name) throw new InvalidOperationException($"'{nameof(parent)}' already contains a child called '{Name}'.");
				Parent.children.Add(this);
			}

		}


		/// <summary>
		/// Adds the given <see cref="IInstance"/> to <see cref="Instances"/> if <paramref name="instance"/>'s <see cref="IInstance.Identifier"/>'s <see cref="string"/> does not exist as a key already.
		/// </summary>
		/// <param name="instance">The <see cref="IInstance"/> to add to this <see cref="Scope"/>'s <see cref="Instances"/>.</param>
		/// <returns>Returns a <see cref="ResultInfo"/> based on if there is already an <see cref="IInstance"/> with <paramref name="instance"/>'s <see cref="IInstance.Identifier"/>'s <see cref="string"/>.</returns>
		public ResultInfo AddInstance(IInstance instance) {

			string identifier = instance.Identifier?.GetText();
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

		private int anonInsts = 0;
		public ITerminalNode MakeAnonymousInstanceName() {

			string text = $"__{anonInsts++}";
			ICharStream stream = CharStreams.fromString(text);
			ITokenSource lexer = new MCSharpLexer(stream);
			ITokenStream tokens = new CommonTokenStream(lexer);
			var parser = new MCSharpParser(tokens) { BuildParseTree = true };
			MCSharpParser.IdentifierContext identifier = parser.identifier();
			return identifier.NAME()[0];

		}

		/// <summary>
		/// Finds the closest parent to this <see cref="Scope"/> with the given <see cref="Name"/>.
		/// </summary>
		/// <param name="name">The <see cref="Name"/> to check for.</param>
		/// <returns>Returns the <see cref="Scope"/> found.</returns>
		public Scope GetFirstParentScopeByName(string name) {

			if(Parent.Name == name) {
				return Parent;
			} else {
				return Parent.GetFirstParentScopeByName(name);
			}

		}

		/// <summary>
		/// Finds the farthest parent to this <see cref="Scope"/> with the given <see cref="Name"/>.
		/// </summary>
		/// <param name="name">The <see cref="Name"/> to check for.</param>
		/// <returns>Returns the <see cref="Scope"/> found.</returns>
		public Scope GetLastParentScopeByName(string name) {

			if(Root.Name == name) {
				return Root;
			} else {

				Scope current = this;
				while((current = current.Parent) != null) {
					if(current.Name == name) {
						return current;
					} else {
						continue;
					}
				}

				return null;

			}

		}

		/// <summary>
		/// Finds the closest parent to this <see cref="Scope"/>.
		/// </summary>
		/// <returns>Returns the <see cref="Scope"/> found.</returns>
		public Scope GetFirstNamedParent() {

			Scope current = this;
			while((current = current.Parent) != null) {
				if(current.Name != null) {
					return current;
				} else {
					continue;
				}
			}

			return null;

		}

	}

}
