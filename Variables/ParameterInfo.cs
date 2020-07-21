using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MCSharp.Variables {
	public class ParameterInfo : IReadOnlyList<(Type Type, bool Reference, Variable Value)> {

		const int ScoreFromCastMatch = 1;
		const int ScoreFromFullMatch = 2;
		
        private IList<(Type Type, bool Reference, Variable Value)> Parameters { get; }
        public (Type Type, bool Reference, Variable Value) this[int index] {
            [DebuggerStepThrough]
            get => Parameters[index];
			[DebuggerStepThrough]
			private set => Parameters[index] = value;
        }
        public int Count => Parameters.Count;

        public ParameterInfo((Type Type, bool Reference)[] description) {
			int length = description.Length;
			Parameters = new (Type Type, bool Reference, Variable Value)[length];
			for(int i = 0; i < length; i++) {
				Variable value;
				if(description[i].Reference) value = null;
				else {
					throw new NotImplementedException();
                }
				Parameters[i] = (description[i].Type, description[i].Reference, value);
			}
		}

		public static implicit operator ParameterInfo((Type Type, bool Reference)[] description) => new ParameterInfo(description);


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
				Type param = Parameters[i].Type, arg = arguments[i].Type;
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
				if(Parameters[i].Reference) {
					Parameters[i] = (parameter.Type, parameter.Reference, argument.Value);
				} else {
					Variable result = parameter.Value.InvokeOperation(Variable.Operation.Set, argument.Value, Compiler.CurrentScriptTrace);
					if(result is null) throw new Variable.InvalidArgumentsException($"Failed set argument {i}.", passed.ScriptTrace);
					Parameters[i] = (parameter.Type, parameter.Reference, result);
				}
			}
		}

        public IEnumerator<(Type Type, bool Reference, Variable Value)> GetEnumerator() => Parameters.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Parameters).GetEnumerator();

    }
}
