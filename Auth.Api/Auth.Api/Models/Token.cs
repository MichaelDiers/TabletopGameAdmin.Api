namespace Auth.Api.Models
{
    using System;

    /// <summary>
    ///     Describes a json web token.
    /// </summary>
    public class Token
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Token" /> class.
        /// </summary>
        /// <param name="value">The value of the token.</param>
        public Token(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            }

            this.Value = value;
        }

        /// <summary>
        ///     Gets the token value.
        /// </summary>
        public string Value { get; }
    }
}
