using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Methods {

    public abstract class MethodGroup {

        public abstract string Call { get; }
        public abstract IReadOnlyCollection<IReadOnlyList<Type>> Overflows { get; }

        public abstract void Invoke(params Variable[] parameters);

    }

    public abstract class MethodGroup<TVariable> where TVariable : Variable {

        public abstract IReadOnlyCollection<IReadOnlyList<Type>> Overflows { get; }

        public abstract void Invoke(TVariable variable, params Variable[] parameters);

    }

}
