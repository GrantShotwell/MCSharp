using MCSharp.Compilation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MCSharp.Variables {
	public class ParameterInfo : IReadOnlyList<ParameterInfo.Parameter> {

		const int ScoreFromCastMatch = 1;
		const int ScoreFromFullMatch = 2;
		
        private IList<Parameter> Parameters { get; }
        public Parameter this[int index] {
            [DebuggerStepThrough]
            get => Parameters[index];
			[DebuggerStepThrough]
			private set => Parameters[index] = value;
        }
		public Parameter this[string name] {
			[DebuggerStepThrough]
			get {
				foreach(var parameter in Parameters) if(parameter.Name == name) return parameter;
				throw new KeyNotFoundException($"No parameter named '{name}' was found.");
			}
		}
        public int Count => Parameters.Count;

        public ParameterInfo(params (bool Reference, string Type, string Name, Compiler.Scope Scope)[] description) {
			int length = description.Length;
			Parameters = new Parameter[length];
			for(int i = 0; i < length; i++) {
				Variable value;
				if(description[i].Reference) {
					value = null;
				} else {
					value = Variable.Initializers[description[i].Type](Access.Pass, Usage.Parameter, description[i].Name, description[i].Scope, Compiler.CurrentScriptTrace);
				}
				Parameters[i] = new Parameter(description[i].Reference, description[i].Type, value, description[i].Name);
			}
			CheckForNameDuplicates();
		}

		/// <summary>Private constructor for setting <see cref="Parameters"/> directly.</summary>
		private ParameterInfo(params Parameter[] parameters) {
			Parameters = parameters;
			CheckForNameDuplicates();
		}

		[DebuggerStepThrough]
		private void CheckForNameDuplicates() {
			for(int i = 0; i < Parameters.Count; i++) {
				var param1 = Parameters[i];
				for(int j = 0; j < Parameters.Count; j++) {
					if(j != i) {
						var param2 = Parameters[j];
						if(param1.Name == param2.Name)
							throw new ArgumentException($"Two parameters have the name '{param1.Name}'.");
					}
				}
			}
		}

		public static implicit operator ParameterInfo((bool Reference, string Type, string Name, Compiler.Scope Scope)[] description) => new ParameterInfo(description);

		public static ParameterInfo CreateFromWilds(ScriptWild wilds, Compiler.Scope scope) {

			var parameters = new Parameter[wilds.Wilds.Count];

			for(int i = 0; i < wilds.Wilds.Count; i++) {
				var wild = wilds.Wilds[i];

				const string syntaxErrorMessage = "Expected arguments in ([type] [name], ...) format.";
				if(!wild.IsWilds || wild.Wilds.Count != 2) throw new Compiler.SyntaxException(syntaxErrorMessage, wild.ScriptTrace);
				IReadOnlyList<ScriptWild> list = wild.Wilds;
				if(!list[0].IsWord) throw new Compiler.SyntaxException(syntaxErrorMessage, wild.ScriptTrace);
				ScriptWord type = list[0].Word;
				if(!list[1].IsWord) throw new Compiler.SyntaxException(syntaxErrorMessage, wild.ScriptTrace);
				ScriptWord name = list[1].Word;

				Variable value = Variable.Initializers[(string)type](Access.Pass, Usage.Parameter, (string)name, scope, name.ScriptTrace);
				parameters[i] = new Parameter(false, value.TypeName, value);

			}

			return new ParameterInfo(parameters);

		}

		public static ParameterInfo CreateForGetAccessor(ScriptString type, Compiler.Scope scope) {
			return new ParameterInfo(new (bool Reference, string Type, string Name, Compiler.Scope Scope)[] { });
		}

		public static ParameterInfo CreateForSetAccessor(ScriptString type, Compiler.Scope scope) {
			Variable value = Variable.Initializers[(string)type](Access.Pass, Usage.Parameter, "value", scope, type.ScriptTrace);
			return new ParameterInfo(new Parameter(false, value.TypeName, value));
		}

		public static (ParameterInfo Overload, int Index) HighestMatch(IReadOnlyList<ParameterInfo> overloads, ArgumentInfo info) {
			ParameterInfo highest = overloads[0];
			int score = 0, index = 0;
			for(int i = 0; i < overloads.Count; i++) {
				ParameterInfo overload = overloads[i];
				int s = overload.MatchScore(info);
				if(score < s) {
					score = s;
					highest = overload;
					index = i;
				}
			}
			return (highest, index);
		}

		public int MatchScore(ArgumentInfo arguments) {
			if(arguments.Count != Parameters.Count) return 0;
			int castMatches = 0, fullMatches = 0;
			for(int i = 0; i < Parameters.Count; i++) {
				string param = Parameters[i].Type, arg = arguments[i].Type;
				if(param == arg) fullMatches++;
				else {
                    if(Variable.Casters.TryGetValue((param, arg), out Variable.Caster[] casters)
                        && casters[0] == null && casters[1] == null) return 0;
                    else castMatches++;
                }
			}
			return (fullMatches * ScoreFromFullMatch) + (castMatches * ScoreFromCastMatch);
		}

		public void Grab(ArgumentInfo passed) {
			if(passed.Count != Parameters.Count)
				throw new Variable.InvalidArgumentsException($"Number of arguments don't match.", passed.ScriptTrace);
			int length = Parameters.Count;
			for(int i = 0; i < length; i++) {

				var parameter = Parameters[i];
				var argument = passed[i];
				Variable result;

				if(Parameters[i].Reference) {
					if(parameter.Type != argument.Type && !argument.Value.TryCast(parameter.Type, out result))
						throw new Variable.InvalidArgumentsException($"Failed set argument {i}.", passed.ScriptTrace);
					else result = argument.Value;
                } else {
					result = parameter.Value.InvokeOperation(Variable.Operation.Set, argument.Value, Compiler.CurrentScriptTrace);
					if(result is null) throw new Variable.InvalidArgumentsException($"Failed set argument {i}.", passed.ScriptTrace);
				}

				Parameters[i] = new Parameter(parameter.Reference, parameter.Type, result, Parameters[i].Name);

			}
		}

		public ParameterInfo Copy() {
			var description = new Parameter[Count];
			for(int i = 0; i < Count; i++) description[i] = new Parameter(Parameters[i].Reference, Parameters[i].Type, Parameters[i].Value, Parameters[i].Name);
			return new ParameterInfo(description);
		}

        public IEnumerator<Parameter> GetEnumerator() => Parameters.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Parameters).GetEnumerator();

		public struct Parameter {

			public bool Reference { get; }
			public string Type { get; }
			public string Name { get; }
			public Variable Value { get; set; }

			public Parameter(bool reference, string type, string name, Compiler.Scope scope) {
				Reference = reference;
				Name = name;
				if(reference) {
					Type = type;
					Value = null;
				} else {
					Type = type;
					Value = Variable.Initializers[type](Access.Pass, Usage.Parameter, name, scope, Compiler.AnonScriptTrace);
				}
			}

			public Parameter(bool reference, string type, Variable value, string name = null) {
				Reference = reference;
				Type = type;
				Name = value?.ObjectName ?? name;
				Value = value;
			}

			public static implicit operator Parameter((bool Reference, string Type, string Name, Compiler.Scope Scope) description)
				=> new Parameter(description.Reference, description.Type, description.Name, description.Scope);

		}

	}

}
