namespace Auth.Api.Validators
{
    using System.ComponentModel.DataAnnotations;
    using Auth.Api.Contracts.Models;

    public class RoleDefinedAttribute : ValidationAttribute
    {
        /// <summary>Determines whether the specified value of the object is valid.</summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        ///     <see langword="true" /> if the specified value is valid; otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The current attribute is malformed.</exception>
        /// <exception cref="T:System.NotImplementedException">
        ///     Neither overload of <see langword="IsValid" /> has been implemented
        ///     by a derived class.
        /// </exception>
        public override bool IsValid(object value)
        {
            try
            {
                var role = (Roles) value;
                return (Roles.All & role) == role;
            }
            catch
            {
                return false;
            }
        }
    }
}
