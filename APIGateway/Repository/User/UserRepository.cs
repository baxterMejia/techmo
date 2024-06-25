using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductDbContext _dbContext;

        public UserRepository(ProductDbContext productDbContext)
        {
            _dbContext = productDbContext;
        }

        public async Task<bool> login(string username, string passBase64)
        {
            // Buscar el usuario por nombre de usuario (o correo electrónico, según tu caso)
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username || u.Email == username);

            // Si no se encontró el usuario, regresar falso
            if (user == null)
                return false;

            // Decodificar la contraseña de Base64
            var pass = DecodeBase64(passBase64);

            // Crear el hash de la contraseña decodificada
            var passHash = CreatePasswordHash(pass);

            // Comparar el hash generado con el hash almacenado en la base de datos
            if (user.PasswordHash == passHash)
            {
                // Si la contraseña es correcta, actualizar el campo StatusSession a true
                user.StatusSession = true;

                // Guardar los cambios en la base de datos
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Logout(string username)
        {
            // Buscar el usuario por nombre de usuario o correo electrónico
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username || u.Email == username);

            // Si no se encontró el usuario, regresar falso
            if (user == null)
                return false;

            // Actualizar el campo StatusSession a false
            user.StatusSession = false;

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> userStateSession(string username)
        {
            // Realiza la validación en base de datos de que la sesión del usuario exista y se encuentre activa
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username || u.Email == username);
            return user != null && user.StatusSession == true;
        }

        // Método auxiliar para decodificar Base64
        private string DecodeBase64(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        // Método auxiliar para crear el hash de la contraseña (SHA256 en este caso)
        private string CreatePasswordHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
