using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSharp.Compilation.Instancing;

[DebuggerDisplay("{ToString(),nq}")]
public class SelfClassInstance : ClassInstance {

	public ClassInstance Reference { get; }

	public SelfClassInstance(Compiler.CompileArguments location, ClassInstance original, string identifier)
	: base(location, original.Type, identifier, original.Pointer, original.FieldObjectives) {

		Reference = original;

	}

	public override string GetSelector(Compiler.CompileArguments location) => "@s";

}
