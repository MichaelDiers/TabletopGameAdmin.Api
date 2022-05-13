namespace Auth.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    ///     Service for creating json web tokens.
    /// </summary>
    public class JwtService : IJwtService
    {
        /// <summary>
        ///     The claim type for user names.
        /// </summary>
        public const string UserNameClaimType = "UserName";

        /// <summary>
        ///     The application settings for json web tokens.
        /// </summary>
        private readonly IAppSettingsJwt appSettingsJwt;

        /// <summary>
        ///     Access the google cloud secret manager.
        /// </summary>
        private readonly ISecretService secretService;

        /// <summary>
        ///     Creates a new instance of the <see cref="JwtService" /> class.
        /// </summary>
        /// <param name="secretService">Access the google cloud secret manager.</param>
        /// <param name="appSettingsJwt">The configuration of json web tokens.</param>
        public JwtService(ISecretService secretService, IAppSettingsJwt appSettingsJwt)
        {
            this.secretService = secretService;
            this.appSettingsJwt = appSettingsJwt;
        }

        /// <summary>
        ///     Create new json web token.
        /// </summary>
        /// <param name="user">The user data for that the token is created.</param>
        /// <returns>A new token.</returns>
        public string Create(IUser user)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(this.secretService.RsaPrivateKey), out _);

            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory {CacheSignatureProviders = false}
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(JwtService.UserNameClaimType, user.UserName)
            };

            claims.AddRange(
                from Roles? value in Enum.GetValues(typeof(Roles))
                where value.HasValue && value != Roles.None && (value & user.Roles) == value
                select new Claim(ClaimTypes.Role, value.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                this.appSettingsJwt.Issuer,
                this.appSettingsJwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(this.appSettingsJwt.ExpiresInMinutes),
                signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        ///     Set the options of the given <see cref="JwtBearerOptions" />.
        /// </summary>
        /// <param name="options">The options that are set.</param>
        public void SetOptions(JwtBearerOptions options)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(this.secretService.RsaPublicKey), out _);

            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = this.appSettingsJwt.Issuer,
                ValidAudience = this.appSettingsJwt.Audience,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                CryptoProviderFactory = new CryptoProviderFactory {CacheSignatureProviders = false}
            };
        }
    }
}
