using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using System.Collections.Generic;

namespace MCSharp.Linkage;

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

	public static IInstance MakeOrGetInstance(this IMethodParameter link, Compiler.CompileArguments location) {

		IInstance instance = link.Instance;
		if(instance != null) return instance;
		else return link.Instance = location.Compiler.DefinedTypes[link.TypeIdentifier].InitializeInstance(location, link.Identifier);

	}

	public static IInstance[] MakeOrGetInstances(this IReadOnlyList<IMethodParameter> links, Compiler.CompileArguments location) {

		int count = links.Count;
		IInstance[] instances = new IInstance[count];
		for(int i = 0; i < count; i++) instances[i] = links[i].MakeOrGetInstance(location);
		return instances;

	}

}
