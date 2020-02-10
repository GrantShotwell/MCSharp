using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Methods {

    public abstract class MethodGroup {

        public abstract string Name { get; }
        public abstract Dictionary<string, IReadOnlyList<Type>> Methods { get; }

        public abstract void Invoke(params Variable[] parameters);

    }

    public abstract class MethodGroup<TVariable> where TVariable : Variable {

        public abstract Dictionary<string, IReadOnlyList<Type>> Methods { get; }

        public abstract void Invoke(TVariable variable, params Variable[] parameters);

    }

}
