using TownerTown.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownerTown.DataAccess
{
    public interface IBusinessRepository
    {
        public Business AddBusiness(Business business);
        public int GetRegisteredBusinessCount();
        List<Business> GetAllReleventBusiness(string location, string category);
        List<string> GetAllCities();
        public List<Category> GetAllCategories();
        public Business GetBusinessByID(int id);
        public Business GetBusinessByUserID(int id);
        public Category GetCategoryByCategoryName(string categoryName);
        public bool UpdateBusiness(string key, string value, int userId);
        public bool UpdateBusinessTimings(int userId, Timings newtimings);
        public bool UpdateBusinessLocation(int userId, Location location);
        public bool AddNewImages(int userId, List<FilePath> images);
        public List<Business> GetAllBusinessByType(string type);
        public bool AddReview(Review review, int userID);
        bool BlockUser(string blockUser, int userID);
        bool UpdatePayment(Business updatedBusiness);
    }
}
