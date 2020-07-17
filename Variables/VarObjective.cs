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
		public override string TypeName => "Objective";
		/// <summary>The scoreboard name of this objective in-game.</summary>
		public string ID { get; }
		/// <summary>The scoreboard type of this objective in-game.</summary>
		public string Type { get; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


		public VarObjective() : base() { }

		public VarObjective(Access access, Usage usage, string name, Compiler.Scope scope, string type) :
		base(access, usage, name, scope) {

#if DEBUG_OUT
			if(NextID == null) throw new Compiler.InternalError($"{nameof(NextID)} was not set.");
			else {
				ID = NextID;
				NextID = null;
			}
#else
			ID = $"mcs.{BaseConverter.Convert(NextID++, 62)}";
#endif

			if(ObjectiveIDs.ContainsKey(ID)) throw new Compiler.InternalError($"Duplicate {nameof(VarObjective)} created.");
			else ObjectiveIDs.Add(ID, this);

			Type = type;

			Methods.Add("GetInt", (arguments) => {
				if(arguments.Length > 1) throw new ArgumentException("Expected at most 1 (Selector) argument for 'Objective.GetInt(...)'.");
				if(arguments.Length == 0) {
					var x = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), scope);
					x.SetValue("@e", GetConstant());
					return x;
				} else if(arguments[0] is VarSelector varSelector || arguments[0].TryCast(out varSelector)) {
					var x = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), scope);
					x.SetValue(varSelector.GetConstant(), GetConstant());
					return x;
				} else throw new ArgumentException($"Could not interpret '{arguments[0]}' as 'Selector'.");
			});

		}


		protected override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			base.Initialize(access, usage, name, scope, trace);

			throw new NotImplementedException();

		}

		public override bool TryCast(Type type, [NotNullWhen(false)] out Variable result) {

			if(type.IsAssignableFrom(typeof(VarInt))) {
				result = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				((PrimitiveType)result).SetValue("@e", GetConstant());
				return true;
			}

			if(type.IsAssignableFrom(typeof(VarBool))) {
				result = new VarBool(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				((PrimitiveType)result).SetValue("@e", GetConstant());
				return true;
			}

			result = null;
			return false;

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
