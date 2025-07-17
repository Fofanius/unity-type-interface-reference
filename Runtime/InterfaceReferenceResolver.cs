using System;
using Object = UnityEngine.Object;

namespace Fofanius.Type
{
    public static class InterfaceReferenceResolver
    {
        public enum NullSourcePolicy
        {
            ReturnDefaultValue = 0,
            ThrowException,
        }

        public static T Resolve<T>(this InterfaceReference<T> source) where T : class =>
            Resolve(source, NullSourcePolicy.ReturnDefaultValue);

        public static T Resolve<T>(this InterfaceReference<T> source, NullSourcePolicy nullSourcePolicy)
            where T : class
        {
            if (source == null)
            {
                switch (nullSourcePolicy)
                {
                    case NullSourcePolicy.ReturnDefaultValue: return null;
                    case NullSourcePolicy.ThrowException: throw new ArgumentNullException(nameof(source));
                    default: throw new ArgumentOutOfRangeException(nameof(nullSourcePolicy), nullSourcePolicy, null);
                }
            }

            if (source.BackingSource is not T)
            {
                switch (nullSourcePolicy)
                {
                    case NullSourcePolicy.ReturnDefaultValue: return null;
                    case NullSourcePolicy.ThrowException:
                        throw new NullReferenceException(
                            $"Source ({source}) does not contain a value of type {typeof(T).Name}!");
                }
            }

            return source.Value;
        }

        public static bool TryResolve<T>(this InterfaceReference<T> reference, out T result) where T : class
        {
            result = Resolve(reference, NullSourcePolicy.ReturnDefaultValue);
            return result is not null;
        }
    }
}