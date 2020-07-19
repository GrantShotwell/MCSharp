using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MCSharp.Variables {
	public class ParameterInfo {

		const int ScoreFromCastMatch = 1;
		const int ScoreFromFullMatch = 2;

		private readonly IList<(Type Type, bool Reference, Variable Value)> parameters;
		public IReadOnlyList<(Type Type, bool Reference, Variable Value)> Parameters => (IReadOnlyList<(Type Type, bool Reference, Variable Value)>)parameters;

		public ParameterInfo((Type Type, bool Reference)[] description) {
			int length = description.Length;
			parameters = new (Type Type, bool Reference, Variable Value)[length];
			for(int i = 0; i < length; i++) parameters[i] = (description[i].Type, description[i].Reference, null);
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

		public int MatchScore(ArgumentInfo info) {
			int castMatches = 0, fullMatches = 0;
			for(int i = 0; i < Parameters.Count; i++) {
				Type param = Parameters[i].Type, arg = info.Arguments[i].Type;
				if(param == arg) fullMatches++;
				else {
					var casters = Variable.Casters[(param, arg)];
					if(casters[0] != null || casters[1] != null) castMatches++;
					else return 0;
				}
			}
			return (fullMatches * ScoreFromFullMatch) + (castMatches * ScoreFromCastMatch);
		}

		public void SendArguments(ArgumentInfo passed) {
			int length = Parameters.Count;
			for(int i = 0; i < length; i++) {
				var parameter = Parameters[i];
				var argument = passed.Arguments[i];
				if(Parameters[i].Reference) {
					parameters[i] = (parameter.Type, parameter.Reference, argument.Value);
				} else {
					Variable result = parameter.Value.InvokeOperation(Variable.Operation.Set, argument.Value, Compiler.CurrentScriptTrace);
					parameters[i] = (parameter.Type, parameter.Reference, result);
				}
			}
		}

	}
}
