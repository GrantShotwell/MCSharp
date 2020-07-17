using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MCSharp.Variables {

	public class VarInt : PrimitiveType {

		public override string TypeName => "int";

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


		public VarInt() : base() { }

		public VarInt(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) { }


		protected override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			base.Initialize(access, usage, name, scope, trace);
			return new VarInt(access, usage, name, scope);
		}

		public override void WriteCopyTo(StreamWriter function, Variable variable) {
			if(variable is VarInt varInt) {
				function.WriteLine($"scoreboard players operation var {varInt.Objective.ID} = var {Objective.ID}");
			} else throw new InvalidArgumentsException($"Unknown how to interpret '{variable}' as '{TypeName}'.", Compiler.CurrentScriptTrace);
		}

		public override string GetJSON() => $"{{\"score\":{{\"name\":\"var\",\"objective\":\"{Objective.ID}\"}}}}";

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace scriptTrace) {

			if(operand is VarInt right || operand.TryCast(out right)) {

				var left = operation == Operation.Set ? null : new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				left?.SetValue(Selector, Objective);

				switch(operation) {

					case Operation.Set:
						new Spy(null, $"scoreboard players operation " +
							$"{Selector.GetConstant()} {Objective.GetConstant()} = " +
							$"{right.Selector.GetConstant()} {right.Objective.GetConstant()}", null);
						return this;

					case Operation.Add:
						new Spy(null, $"scoreboard players operation " +
							$"{left.Selector.GetConstant()} {left.Objective.GetConstant()} += " +
							$"{right.Selector.GetConstant()} {right.Objective.GetConstant()}", null);
						return left;

					case Operation.Subtract:
						new Spy(null, $"scoreboard players operation " +
							$"{left.Selector.GetConstant()} {left.Objective.GetConstant()} -= " +
							$"{right.Selector.GetConstant()} {right.Objective.GetConstant()}", null);
						return left;

					case Operation.Multiply:
						new Spy(null, $"scoreboard players operation " +
							$"{left.Selector.GetConstant()} {left.Objective.GetConstant()} *= " +
							$"{right.Selector.GetConstant()} {right.Objective.GetConstant()}", null);
						return left;

					case Operation.Divide:
						new Spy(null, $"scoreboard players operation " +
							$"{left.Selector.GetConstant()} {left.Objective.GetConstant()} /= " +
							$"{right.Selector.GetConstant()} {right.Objective.GetConstant()}", null);
						return left;

					case Operation.Modulo:
						new Spy(null, $"scoreboard players operation " +
							$"{left.Selector.GetConstant()} {left.Objective.GetConstant()} %= " +
							$"{right.Selector.GetConstant()} {right.Objective.GetConstant()}", null);
						return left;

					default: return base.InvokeOperation(operation, operand, scriptTrace);

				}

			} else throw new Compiler.SyntaxException($"Cannot cast '{operand}' into '{TypeName}'.", scriptTrace);

		}

		public override bool TryCast(Type type, [NotNullWhen(false)] out Variable result) {
			if(type.IsAssignableFrom(typeof(VarBool))) {
				result = new VarBool(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				((PrimitiveType)result).SetValue(Selector, Objective);
				return true;
			} else {
				return base.TryCast(type, out result);
			}
		}

	}

}
