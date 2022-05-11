namespace Auth.Api.Services
{
    using System;
    using Auth.Api.Contracts.Services;
    using BCrypt.Net;

    /// <summary>
    ///     Hash and verify passwords.
    /// </summary>
    public class PasswordHashService : IPasswordHashService
    {
        /// <summary>
        ///     Create a hash from the given password.
        /// </summary>
        /// <param name="password">The input of the hash function.</param>
        /// <returns>The hashed password.</returns>
        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));
            }

            return BCrypt.HashPassword(password);
        }

        /// <summary>
        ///     Verifies that the <paramref name="password" /> matches the <paramref name="passwordHash" />.
        /// </summary>
        /// <param name="password">The <paramref name="password" /> that is verified against the <paramref name="passwordHash" />.</param>
        /// <param name="passwordHash">The hashed password.</param>
        /// <returns>True if <paramref name="password" /> and <paramref name="passwordHash" /> do match and false otherwise.</returns>
        public bool VerifyHash(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(passwordHash));
            }

            return BCrypt.Verify(password, passwordHash);
        }
    }
}
