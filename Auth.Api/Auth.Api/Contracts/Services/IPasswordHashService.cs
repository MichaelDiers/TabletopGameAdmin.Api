namespace Auth.Api.Contracts.Services
{
    /// <summary>
    ///     Hash and verify passwords.
    /// </summary>
    public interface IPasswordHashService
    {
        /// <summary>
        ///     Create a hash from the given password.
        /// </summary>
        /// <param name="password">The input of the hash function.</param>
        /// <returns>The hashed password.</returns>
        string Hash(string password);

        /// <summary>
        ///     Verifies that the <paramref name="password" /> matches the <paramref name="passwordHash" />.
        /// </summary>
        /// <param name="password">The <paramref name="password" /> that is verified against the <paramref name="passwordHash" />.</param>
        /// <param name="passwordHash">The hashed password.</param>
        /// <returns>True if <paramref name="password" /> and <paramref name="passwordHash" /> do match and false otherwise.</returns>
        bool VerifyHash(string password, string passwordHash);
    }
}
