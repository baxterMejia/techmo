using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Metodo que inicia sesion del usuario
        /// </summary>
        /// <param name="username">Usuario</param>
        /// <param name="pass">Contraseña</param>
        /// <returns></returns>
        Task<bool> login(string username, string pass);

        /// <summary>
        /// Generacion de token
        /// </summary>
        /// <param name="username">Generacion de Token</param>
        /// <returns></returns>
        string GenerateToken(string username);

        /// <summary>
        /// Método que cierra sesión del usuario.
        /// </summary>
        /// <param name="username">Usuario</param>
        /// <returns></returns>
        Task<bool> Logout(string username);
    }
}
