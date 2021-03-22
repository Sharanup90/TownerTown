using System;
using System.Collections.Generic;
using System.Text;
using TownerTown.Entities;

namespace TownerTown.Service
{
    public interface IBusinessService
    {
        Business AddNewBusiness(Business business);
        List<Business> GetAllReleventBusiness(string location, string category);
        List<string> GetALLCities();
        public List<Category> GetAllCategories();
        public Business GetBusinessByUserID(int id);
        public Business GetBusinessByID(int id);
        public bool UpdateBusiness(string key, string value, int userId);
        public bool UpdateBusinessLocation(int userId, Location location);
        public bool UpdateBusinessTimings(int userId, Timings newtimings);
        public bool AddNewImages(int userId, List<FilePath> images);
        public List<Business> GetAllBusinessByType(string type);
        public bool AddReview(Review review, int userID);
        bool BlockUser(string blockUser, int userID);
        bool UpdatePayment(Business updatedBusiness);
    }
}
