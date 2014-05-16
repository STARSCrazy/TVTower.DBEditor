using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CodeKnight.Core
{
    public class WeakReference<T> : WeakReference
    {
        public WeakReference(T target) : base(target) { }
        public WeakReference(T target, bool trackResurrection) : base(target, trackResurrection) { }
        protected WeakReference(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public virtual T TargetGeneric
        {
            get { return (T)Target; }
            set { Target = value; }
        }

    }
}