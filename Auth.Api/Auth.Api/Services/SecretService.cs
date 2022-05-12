namespace Auth.Api.Services
{
    using System.Threading.Tasks;
    using Auth.Api.Contracts.Services;

    /// <summary>
    ///     Access the google cloud secret manager.
    /// </summary>
    public class SecretService : ISecretService
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="SecretService" /> class.
        /// </summary>
        /// <param name="rsaPrivateKey">A rsa private key.</param>
        /// <param name="rsaPublicKey">The matching rsa public key for the <paramref name="rsaPrivateKey" />.</param>
        private SecretService(string rsaPrivateKey, string rsaPublicKey)
        {
            this.RsaPrivateKey = rsaPrivateKey;
            this.RsaPublicKey = rsaPublicKey;
        }

        /// <summary>
        ///     Gets the rsa private key.
        /// </summary>
        public string RsaPrivateKey { get; }

        /// <summary>
        ///     Gets the rsa public key.
        /// </summary>
        public string RsaPublicKey { get; }

        /// <summary>
        ///     Creates an <see cref="ISecretService" />.
        /// </summary>
        /// <returns>A <see cref="Task{T}" /> whose result is a <see cref="ISecretService" />.</returns>
        public static async Task<ISecretService> Create()
        {
            await Task.CompletedTask;
            const string publicKey =
                "MIICCgKCAgEAyNjZREwNHIRi00KZQ7OxXQ0IPF6iARRSc+QGHL97FXSAgveuotTpsb1sVHp2N3kZVPA/U+yobzjZV1QGanvKkZY4EGOyTHXZXJKqKBElpU2AmILngH18bWe/8z1Ll+8pLZuyJjLIsvJ4AUWla2H8ByCSnq2U82syauGLmnN0t3SCWum45qqF2gnkowOF8qv8pGTX6RgX20KUM6qkEj6Z1ejo9jr/eMdLHQ/imwJk2LNijlJsdwrymH7ABmzRoebrcHZXbJBplQ1iR+6DMAtpc/3atwhug62Vgn85RsXVVVNiceKg0jxP9Ln5HERfqVmSmyRVihbEMx1LywNTZ8snvuwlsecG/iaqP6EZzqbesBi6dDXceDrIIFMS/cvBEFwGHL0AcnBKgcdeC1n3h5e9qobVSejhU/eudWfDZkuoEVJxj1XxyLg8NuQqc7DHnBC2vWKsyJaQSBY5NBiAmbVLwmcAP+/SGVW+WmoO86qaa4QJjE1uYXkzyr/653RIlrxrq21L9xtKw8KGHDVad/2cL9qZZ0rTGQ35pr6t59FydbE9CgOnzHOZNOgXsR8UOeBkGYp9F2W0Lo9vIxKrQmn7sWiuQggyUl+Zp+8IVBFEEvFoGW1ArKzKLan1IqpZ4NalvG1Aj7DmuxdTZ7roMFLyIq9h4+vi6qABN09/CyYtuCECAwEAAQ==";
            const string privateKey =
                "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";
            return new SecretService(privateKey, publicKey);
        }
    }
}
