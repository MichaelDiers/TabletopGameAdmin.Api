namespace Auth.Api.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="string" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Normalize the name of a user.
        /// </summary>
        /// <param name="s">The <see cref="string" /> to be normalized.</param>
        /// <returns>The normalized user name.</returns>
        public static string NormalizeUserName(this string s)
        {
            return s.ToUpperInvariant();
        }

        /// <summary>
        ///     Checks if two user names are equal.
        /// </summary>
        /// <param name="a">The user name to be compared.</param>
        /// <param name="b">The other user name to be compared.</param>
        /// <returns>True if the user names match and false otherwise.</returns>
        public static bool UserNameEquals(this string a, string b)
        {
            return a.NormalizeUserName() == b.NormalizeUserName();
        }
    }
}
