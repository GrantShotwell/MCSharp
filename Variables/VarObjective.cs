using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;

namespace MCSharp.Variables {

	/// <summary>
	/// Represents a Minecraft scoreboard objective.
	/// </summary>
	public class VarObjective : Variable {

		/// <summary>
		/// A collection of every <see cref="VarObjective"/> organized by their <see cref="ID"/>.
		/// </summary>
		private static Dictionary<string, VarObjective> ObjectiveIDs { get; } = new Dictionary<string, VarObjective>();

#if DEBUG_OUT
		public static string NextID { get; set; }
#else
		public static int NextID { get; private set; }
#endif

		public override int Order => base.Order - 10;
		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "objective";
		/// <summary>The scoreboard name of this objective in-game.</summary>
		public string ID { get; private set; }
		/// <summary>The scoreboard type of this objective in-game.</summary>
		public string Type { get; private set; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


		public VarObjective() : base() { }
		public VarObjective(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) { }


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarObjective(access, usage, name, scope);

		private static readonly ParameterInfo[] ConstructorOverloads = new ParameterInfo[] {
			new (Type Type, bool Reference)[] { },
			new (Type Type, bool Reference)[] { (typeof(VarString), true) }
		};
		public override Variable Construct(ArgumentInfo arguments) {
			(ParameterInfo match, int index) = ParameterInfo.HighestMatch(ConstructorOverloads, arguments);
			match.Grab(arguments);

			string type;
			switch(index) {

				case 1:
					type = "dummy";
					goto Construct;
				case 2:
					type = (match[0].Value as VarString).GetConstant();
					goto Construct;


					Construct:
					var value = new VarObjective(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
					value.Type = type;
#if DEBUG_OUT
					if(NextID == null) throw new Compiler.InternalError($"{nameof(NextID)} was not set.");
					else { value.ID = NextID; NextID = null; }
#else
					value.ID = $"mcs.{BaseConverter.Convert(NextID++, 62)}";
#endif
					if(ObjectiveIDs.ContainsKey(value.ID)) throw new Compiler.InternalError($"Duplicate {nameof(VarObjective)} created.");
					else ObjectiveIDs.Add(value.ID, this);
					value.Constructed = true;
					return value;


				default: throw new MissingOverloadException("Objective constructor", index, arguments);
			}
		}

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {
				case Operation.Set:
					if(operand is VarObjective right || operand.TryCast(out right)) {
						if(ID == null && Type == null) {
							ID = right.ID;
							Type = right.Type;
							return this;
						} else {
							throw new Exception();
						}
					} else throw new Compiler.SyntaxException($"Cannot cast '{operand}' into '{TypeName}'.", trace);
				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public override IDictionary<Type, Caster> GetCasters_To() {
			IDictionary<Type, Caster> casters = base.GetCasters_To();
			casters.Add(GetType(), value => {
				var result = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				result.SetValue((VarSelector)"@e", this);
				return result;
			});
			casters.Add(typeof(VarBool), value => {
				var result = new VarBool(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				result.SetValue((VarSelector)"@e", this);
				return result;
			});
			return casters;
		}

		public override void WritePrep(StreamWriter function) {
			base.WritePrep(function);
			function.WriteLine($"scoreboard objectives add {ID} {Type}");
		}

		public override void WriteDemo(StreamWriter function) {
			base.WriteDemo(function);
			function.WriteLine($"scoreboard objectives remove {ID}");
		}

		public override string GetConstant() => ID;

		public static void ResetID() {
			ObjectiveIDs.Clear();
#if DEBUG_OUT
			NextID = null;
#else
			NextID = 0;
#endif
		}
	}

}
