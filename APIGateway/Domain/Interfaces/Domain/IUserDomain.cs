using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Domain
{
    public interface IUserDomain
    {
        /// <summary>
        /// Metodo de autenticacion del usuario
        /// </summary>
        /// <param name="credentials">Credenciales de Usuario</param>
        /// <returns></returns>
        Task<string> Authenticate(UserCredentialsDTO credentials);

        /// <summary>
        /// Método que cierra sesión del usuario.
        /// </summary>
        /// <param name="username">Usuario</param>
        /// <returns></returns>
        Task<bool> Logout(string username);

        /// <summary>
        /// Metodo para registrar usuarios
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        Task<string> RegisterUser(UserRegistrationDTO credentials);

        /// <summary>
        /// Metodo que consulta los datos del usuario loggeado
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        Task<string> GetUserData(UserCredentialsDTO credential, string token);

    }
}
