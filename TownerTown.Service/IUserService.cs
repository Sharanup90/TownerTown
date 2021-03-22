using System;
using System.Collections.Generic;
using System.Text;
using TownerTown.Entities;

namespace TownerTown.Service
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        public User GetByIdWithPassword(int id);
        List<string> GetAllUserNames();
    }
}
