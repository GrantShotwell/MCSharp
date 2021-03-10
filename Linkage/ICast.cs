using MCSharp.Linkage.Minecraft;

namespace MCSharp.Linkage {

	public interface ICast {

		public bool Implicit { get; }

		public bool Explicit { get; }

		public IType ReferenceType { get; }

		public IType TargetType { get; }

		public IFunction Function { get; }

	}

}
