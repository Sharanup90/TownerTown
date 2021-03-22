using TownerTown.Entities;
using System.Collections.Generic;

namespace TownerTown.DataAccess
{
    public interface IUserRepository
    {
        public List<User> GetAllUSers();
        public User GetById(int userID);
        public List<string> GetAllUserNames();
    }
}