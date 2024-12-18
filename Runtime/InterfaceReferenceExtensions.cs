namespace Fofanius.Type
{
    public static class InterfaceReferenceExtensions
    {
        public static T GetValueOrDefault<T>(this InterfaceReference<T> reference) where T : class
        {
            return reference?.GetValue();
        }

        public static bool TryGetValue<T>(this InterfaceReference<T> reference, out T result) where T : class
        {
            result = default;

            if (reference is null) return false;
            result = reference.GetValue();

            return result is not null;
        }
    }
}