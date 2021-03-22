using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TownerTown.DataAccess;
using TownerTown.Entities;
using TownerTown.Helpers;

namespace TownerTown.Service
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { ID = 1, FirstName = "Admin", LastName = "User", UserName = "admin", Password = "admin", Role = Role.Admin },
            new User { ID = 2, FirstName = "Normal", LastName = "User", UserName = "user", Password = "user", Role = Role.User }
        };

        private readonly AppSettings _appSettings;
        IUserRepository userRepo;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            userRepo = userRepository;
        }

        public UserService()
        {
        }

        public User Authenticate(string username, string password)
        {
            _users = userRepo.GetAllUSers();
            var user = _users.SingleOrDefault(x => x.UserName == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }


        public IEnumerable<User> GetAll()
        {
            _users = userRepo.GetAllUSers();
            return _users.WithoutPasswords();
        }

        public User GetById(int id)
        {
            var user = userRepo.GetById(id);
            return user.WithoutPassword();
        }

        public User GetByIdWithPassword(int id)
        {
            var user = userRepo.GetById(id);
            return user;
        }
        public List<string> GetAllUserNames()
        {
            return userRepo.GetAllUserNames();
        }
    }
}
