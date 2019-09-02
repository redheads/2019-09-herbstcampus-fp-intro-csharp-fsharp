namespace CSharpWorkshop
{
    /// <summary>
    ///     Actual rules are not important for this demo.
    ///     That is why they are summarized here
    ///     so we can use the same rules for different approaches.
    /// </summary>
    public static class RulesExtensions
    {
        public static bool IsNonEmpty(this string s)
            => !string.IsNullOrWhiteSpace(s);

        public static bool IsEmpty(this string s)
            => !s.IsNonEmpty();

        public static bool IsValidId(this int i)
            => i > 0;

        public static bool IsValidZipcode(this string s)
            => s != null && s.Length >= 3;
    }
}