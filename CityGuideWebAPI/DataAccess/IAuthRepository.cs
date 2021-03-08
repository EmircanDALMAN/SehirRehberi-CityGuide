using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityGuideWebAPI.Entities;

namespace CityGuideWebAPI.DataAccess
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}
