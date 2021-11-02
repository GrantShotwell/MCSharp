using System;
using System.Collections.Generic;
using System.Text;
using MCSharp.Compilation;
using MCSharp.Linkage.Predefined;

namespace MCSharp.Linkage.Extensions {

	/// <summary>
	/// Represents an extension to the linker that adds <see cref="PredefinedType"/>s.
	/// </summary>
	public abstract class LinkerExtension {

		/// <summary>
		/// The <see cref="Compiler"/> this extension adds to.
		/// </summary>
		public Compiler Compiler { get; }


		/// <summary>
		/// Creates a new <see cref="LinkerExtension"/>.
		/// </summary>
		/// <param name="compiler">The <see cref="Compilation.Compiler"/> this extension will add to.</param>
		protected LinkerExtension(Compiler compiler) {
			Compiler = compiler;
		}


		/// <summary>
		/// Create <see cref="PredefinedType"/>(s) and add them to <see cref="Compiler.DefinedTypes"/>.
		/// </summary>
		public abstract void CreatePredefinedTypes(out Action<Compiler.CompileArguments> onLoad, out Action<Compiler.CompileArguments> onTick);

	}

}
