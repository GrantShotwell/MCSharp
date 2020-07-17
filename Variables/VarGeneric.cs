using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MCSharp.Compilation.ScriptObject;

namespace MCSharp.Variables {

	public abstract class VarGeneric : Variable {

		public ScriptObject ScriptClass { get; }
		public override string TypeName => ScriptClass.Alias;
		public new IList<Constructor> Constructors { get; } = new List<Constructor>();
		public static HashSet<ScriptConstructor> CompiledConstructors { get; } = new HashSet<ScriptConstructor>();


		public VarGeneric() : base() { }

		/// <summary>
		/// Constructor for a static object.
		/// </summary>
		public VarGeneric(ScriptObject script)
		: base(script.Access, script.Usage, script.Alias, Compiler.RootScope) {

			ScriptClass = script;
			Compiler.StaticClassObjects.Add(TypeName, this);
			Initializers.Add(TypeName, Initialize);
			Variable.Constructors.Add(TypeName, Construct);
			foreach(var member in from ScriptMember member in ScriptClass.Values where (member is ScriptConstructor) || !(member is ScriptMethod) || member.Usage == Usage.Static select member)
				AddMember(member);

		}


		/// <summary>
		/// Constructor for a non-static object.
		/// </summary>
		public VarGeneric(Access access, Usage usage, string name, Compiler.Scope scope, ScriptObject script)
		: base(access, usage, name, scope) {

			ScriptClass = script;
			foreach(var member in from ScriptMember member in ScriptClass.Values where member.Usage != Usage.Static select member)
				AddMember(member);

		}


		public void AddMember(ScriptMember member) {
			switch(member) {
				case ScriptProperty property:
					Properties.Add(property.Alias, CompileProperty(property));
					break;
				case ScriptConstructor constructor:
					Constructors.Add(CompileConstructor(constructor));
					break;
				case ScriptMethod function:
					Methods.Add(function.Alias, CompileMethod(function));
					break;
				case ScriptField field:
					Fields.Add(field.Alias, CompileField(field));
					break;
				default: throw new Compiler.InternalError($"VarGeneric has not implemented member type '{member.GetType().Name}'.");
			}
		}

		public abstract (GetProperty get, SetProperty set) CompileProperty(ScriptProperty property);
		public abstract MethodDelegate CompileMethod(ScriptMethod function);
		public abstract Variable CompileField(ScriptField field);
		public abstract Constructor CompileConstructor(ScriptConstructor constructor);

	}

}
