using OpenSpace.Models;
using OpenSpace.Request;
using OpenSpace.Sevices.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenSpace.Sevices.Implementation
{
    public class LoginService : ILoginService
    {
        private static List<User> Users;

        public LoginService()
        {
            Users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name= "Jhenifer",
                    Password = "lux123",
                    UserName = "jsantos",
                    CellPhone ="(19)95555-5555"
                }
            };
        }

        public User Auth(LoginRequest request)
        {
            return Users.FirstOrDefault(u => request.Password == u.Password && request.UserName == u.UserName);
        }

        
    }
}
