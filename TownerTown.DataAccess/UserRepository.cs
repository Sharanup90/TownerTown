using TownerTown.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TownerTown.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext context;

        public UserRepository (ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }

        public List<User> GetAllUSers()
        {
            List<User> users = new List<User>();
            users = context.Users.ToList();
            return users;
        }
        public User GetById(int userID)
        {
            return context.Users.Where(u => u.ID == userID).FirstOrDefault();
        }

        public List<string> GetAllUserNames()
        {
            List<string> userNames = new List<string>();
            userNames =  context.Users.Select(p => p.UserName).ToList();
            return userNames;
        }

    }
}

