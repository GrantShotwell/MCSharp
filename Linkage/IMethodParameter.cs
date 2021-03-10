using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using System.Collections.Generic;

namespace MCSharp.Linkage {

	public interface IMethodParameter {

		/// <summary>
		/// 
		/// </summary>
		public string TypeIdentifier { get; }

		/// <summary>
		/// 
		/// </summary>
		public string Identifier { get; }

		/// <summary>
		/// 
		/// </summary>
		public IInstance Instance { get; set; }

	}

	public static class IMethodParameterExtensions {

		public static IInstance MakeOrGetInstance(this IMethodParameter link, Compiler compiler, FunctionWriter writer, Scope scope) {

			#region Argument Checks
			if(link is null)
				throw new System.ArgumentNullException(nameof(link));
			if(compiler is null)
				throw new System.ArgumentNullException(nameof(compiler));
			if(writer is null)
				throw new System.ArgumentNullException(nameof(writer));
			if(scope is null)
				throw new System.ArgumentNullException(nameof(scope));
			#endregion

			IInstance instance = link.Instance;
			if(instance != null) return instance;
			else return link.Instance = compiler.DefinedTypes[link.TypeIdentifier].InitializeInstance(writer, scope, link.Identifier);

		}

		public static IInstance[] MakeOrGetInstances(this IReadOnlyList<IMethodParameter> links, Compiler compiler, FunctionWriter writer, Scope scope) {

			#region Argument Checks
			if(links is null)
				throw new System.ArgumentNullException(nameof(links));
			if(compiler is null)
				throw new System.ArgumentNullException(nameof(compiler));
			if(writer is null)
				throw new System.ArgumentNullException(nameof(writer));
			if(scope is null)
				throw new System.ArgumentNullException(nameof(scope));
			#endregion

			int count = links.Count;
			IInstance[] instances = new IInstance[count];
			for(int i = 0; i < count; i++) instances[i] = links[i].MakeOrGetInstance(compiler, writer, scope);
			return instances;

		}

	}

}
