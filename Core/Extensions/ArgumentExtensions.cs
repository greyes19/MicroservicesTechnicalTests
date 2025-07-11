namespace Core.Extensions
{
    public static class ArgumentExtensions
    {
        public static void ValidateArgumentOrThrow(this Guid value, string paramName)
        {
            if (value == default) throw new ArgumentException("Cannot be a Guid.Empty value", paramName);
        }

        public static void ValidateArgumentOrThrow<T>(this T value, string paramName) where T : class
        {
            if (value is string strValue)
            {
                if (string.IsNullOrWhiteSpace(strValue)) throw new ArgumentNullException(paramName);

            }
            else if (value == default)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
