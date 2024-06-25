using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> userStateSession(string username)
        {
            // Realiza la validación en base de datos de que la sesión del usuario exista y se encuentre activa
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username || u.Email == username);
            return user != null && user.StatusSession == true;
        }
    }
}
