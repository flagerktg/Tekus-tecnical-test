using Application.DTOs;

namespace Tekus.Application.Interfaces
{
    public interface ISecurityService
    {
        /// <summary>
        /// Checks whether the credentials provided are valid.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">Password</param>
        /// <returns>User security information</returns>
        /// <exception cref="Exceptions.TekusException">When there is no user with the provided email</exception>
        /// <exception cref="Exceptions.TekusException">When the provided credentials are not valid</exception>
        UserDto CheckCredentials(string email, string password);

        /// <summary>
        /// Updates user credentials.
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <param name="password">User password</param>
        /// <exception cref="Exceptions.TekusException">When there is no user with the provided email</exception>
        /// <exception cref="Exceptions.TekusException">When the password does not meet required policies</exception>
        void UpdateCredentials(long userId, string password);

        /// <summary>
        /// Checks whether a password meets the required complexity.
        /// </summary>
        /// <param name="password">Password to be checked</param>
        /// <returns>TRUE if the password meets the required complexity. FALSE otherwise</returns>
        bool CheckPasswordComplexity(string password);
    }
}
