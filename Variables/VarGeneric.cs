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
		public new IList<(Constructor Constructor, ParameterInfo Parameters)> Constructors { get; } = new List<(Constructor Constructor, ParameterInfo Parameters)>();
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
			foreach(var member in from ScriptMember member in ScriptClass.Values where member.Usage == Usage.Static || member is ScriptConstructor select member)
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
					AddProperty(property);
					break;
				case ScriptConstructor constructor:
					AddConstructor(constructor);
					break;
				case ScriptMethod function:
					AddMethod(function);
					break;
				case ScriptField field:
					AddField(field);
					break;
				default: throw new Compiler.InternalError($"VarGeneric has not implemented member type '{member.GetType().Name}'.");
			}
		}

		public abstract void AddField(ScriptField field);
		public abstract void AddProperty(ScriptProperty property);
		public abstract void AddConstructor(ScriptConstructor constructor);
		public abstract void AddMethod(ScriptMethod method);

	}

}
