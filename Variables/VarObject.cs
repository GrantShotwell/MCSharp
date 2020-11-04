using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static MCSharp.Compilation.ScriptObject;

namespace MCSharp.Variables {

	public class VarObject : VarGeneric {

		private static VarObjective objectId;
		public static VarObjective ObjectIdObjective {
			get {
				if(objectId is null) {
					var objective = new VarObjective(Access.Public, Usage.Static, GetNextHiddenID(), Compiler.RootScope);
					objective.Construct(new ArgumentInfo(new Variable[] { (VarString)"dummy" }, Compiler.AnonScriptTrace));
					objectId = objective;
				}
				return objectId;
			}
		}

		public override ICollection<Access> AllowedAccessModifiers => new Access[] {
			Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] {
			Usage.Abstract, Usage.Virtual, Usage.Override, Usage.Default, Usage.Static };

		public VarSelector Selector { get; private set; }
		public VarInt ObjectId { get; private set; }


		public VarObject() : base() { objectId = null; }
		public VarObject(ScriptObject script) : base(script) { }
		public VarObject(Access access, Usage usage, string name, Compiler.Scope scope, ScriptObject script)
		: base(access, usage, name, scope, script) {

		}


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarObject(access, usage, name, scope, ScriptClass);

		public override Variable Construct(ArgumentInfo passed) => throw new NotImplementedException();

		public override void ConstructAsPasser() => throw new NotImplementedException();
		public override void AddField(ScriptField field) => throw new NotImplementedException();
		public override void AddProperty(ScriptProperty property) => throw new NotImplementedException();
		public override void AddConstructor(ScriptConstructor constructor) => throw new NotImplementedException();
		public override void AddMethod(ScriptMethod method) => throw new NotImplementedException();

	}

}
