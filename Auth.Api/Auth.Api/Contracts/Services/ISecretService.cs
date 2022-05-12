namespace Auth.Api.Contracts.Services
{
    /// <summary>
    ///     Access the google cloud secret manager.
    /// </summary>
    public interface ISecretService
    {
        /// <summary>
        ///     Gets the rsa private key.
        /// </summary>
        string RsaPrivateKey { get; }

        /// <summary>
        ///     Gets the rsa public key.
        /// </summary>
        string RsaPublicKey { get; }
    }
}
