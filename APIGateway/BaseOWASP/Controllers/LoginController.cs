using DataAccess.Models;
using Domain.DTO;
using Domain.Interfaces.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BaseOWASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserDomain _userDomain;     
        public LoginController(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }

        /// <summary>
        /// Login de la Aplicacion
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost("LoginValidate")]
        public async Task<ActionResult<string>> Login([FromBody] UserCredentialsDTO requestDto)
        {            
            //Autenticacion de Credenciales de Usuario
            string response = await _userDomain.Authenticate(requestDto);
            if (response.Contains("Incorrecto"))
            {
                return Unauthorized("Credenciales incorrectas.");
            }
            else
            {

                return Ok(new { token = response }); // Devuelve el token dentro de un objeto JSON
            }
        }

        /// <summary>
        /// LogOut de la Aplicacion
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost("LogOut")]
        public async Task<ActionResult<string>> LogOut([FromBody] UserCredentialsDTO requestDto)
        {
            //Autenticacion de Credenciales de Usuario
            bool response = await _userDomain.Logout(username: requestDto.Username);
            if (response)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest("Logout failed");
            }
        }

        /// <summary>
        /// Servicio de autorizacion de prueba
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Validacion")]
        public async Task<ActionResult<string>> Validacion([FromBody] string requestDto)
        {
            if (string.IsNullOrEmpty(requestDto))
            {
                // Lanzamos una excepción que será capturada por el middleware de manejo de errores
                throw new ArgumentException("El valor del requestDto no puede ser nulo o vacío");
            }

            return requestDto;
        }
    }
}
