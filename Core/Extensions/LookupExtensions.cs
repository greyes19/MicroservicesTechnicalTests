namespace Core.Extensions
{
    public static class LookupExtensions
    {
        public static void ValidateExistOrThrow<TId, T>(this T item, TId id) where TId : struct
        {
            if (item is null)
            {

                throw new ArgumentException($"{typeof(T).Name} with id {id} cannot be found.");
            }
        }
    }
}
