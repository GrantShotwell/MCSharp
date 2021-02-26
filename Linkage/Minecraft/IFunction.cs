using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	public interface IFunction : IDisposable {

		public IGenericParameter[] GenericParameters { get; }

		public IMethodParameter[] MethodParameters { get; }

		public IStatement[] Statements { get; }

		public string ReturnTypeIdentifier { get; }

	}

}
