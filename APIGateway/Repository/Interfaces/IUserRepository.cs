using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public  interface IUserRepository
    {
        /// <summary>
        /// Metodo que valida el estado de la sesion del usuario.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> userStateSession(string username);

        /// <summary>
        /// Metodo que inicia sesion del usuario
        /// </summary>
        /// <param name="username">Usuario</param>
        /// <param name="pass">Contraseña</param>
        /// <returns></returns>
        Task<bool> login(string username, string pass);

        /// <summary>
        /// Método que cierra sesión del usuario.
        /// </summary>
        /// <param name="username">Usuario</param>
        /// <returns></returns>
        Task<bool> Logout(string username);
    }
}
