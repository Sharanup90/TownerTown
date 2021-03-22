using TownerTown.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TownerTown.DataAccess
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly ApplicationDBContext context;
        public BusinessRepository(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }
        public Business AddBusiness(Business business)
        {
            context.Business.Add(business);
            context.SaveChanges();

            return context.Business.Where(b => b.ID == business.ID).FirstOrDefault();
        }

        public List<string> GetAllCities()
        {
            List<string> cities = new List<string>();
           var citie = context.Business.Select(p => p.Address.City)
                                  .Distinct().ToList();
            return citie;
        }

        public List<Business> GetAllReleventBusiness(string location, string category)
        {
            List<Business> releventBusinesses;

            releventBusinesses = context.Business
                 .Include(b => b.Address)
                .Include(b => b.BusinessOwner)
                .Include(b => b.Category)
                .Include(b => b.Contacts)
                .Include(b => b.Images)
                .Include(b => b.Timings)
                .Include(b => b.Reviews)
               .Where(b => b.Address.City == location && b.Category.CategoryName == category)
               .ToList();

            //releventBusinesses = context.Business
            //    .Include(b => b.Address)
            //    .Include(b => b.BusinessOwner)
            //    .Include(b => b.Category)
            //    .Include(b => b.Contacts)
            //    .Include(b => b.Images)
            //    .Include(b => b.Payment)
            //    .Include(b => b.Timings)
            //    .Include(b => b.Reviews)
            //    .Where(b => b.Address.City == location && b.Category.CategoryName == category)                
            //    .ToList();
            
            if(releventBusinesses == null)
            {
                releventBusinesses = context.Business
                .Include(b => b.Address)
                .Include(b => b.BusinessOwner)
                .Include(b => b.Category)
                .Include(b => b.Contacts)
                .Include(b => b.Images)
                .Include(b => b.Payment)
                .Include(b => b.Timings)
                .Include(b => b.Reviews)
                .Where(b => b.Address.City.Contains(location) && b.Category.CategoryName.Contains(category))
                .ToList();
            }

            if(releventBusinesses.Where(b => b.Reviews != null).Count() > 10)
            {
                releventBusinesses = releventBusinesses.OrderByDescending(b => b.OverallRating).ToList();
            }else
            {
                releventBusinesses = releventBusinesses.OrderByDescending(b => b.RegisteredOn).ToList();
            }

            return releventBusinesses;
        }

        public List<Category> GetAllCategories()
        {
            return context.Category.ToList();
        }

        public int GetRegisteredBusinessCount()
        {
            return context.Business.Count();
        }

        public Business GetBusinessByID(int id)
        {
            return context.Business
                   .Include(b => b.Address)
                   .Include(b => b.BusinessOwner)
                   .Include(b => b.Category)
                   .Include(b => b.Contacts)
                   .Include(b => b.Images)
                   //.Include(b => b.Payment)
                   .Include(b => b.Timings)
                   .Include(c => c.Timings.Monday)
                   .Include(c => c.Timings.Tuesday)
                   .Include(c => c.Timings.Wednesday)
                   .Include(c => c.Timings.Thursday)
                   .Include(c => c.Timings.Friday)
                   .Include(c => c.Timings.Saturday)
                   .Include(c => c.Timings.Sunday)
                   .Include(b => b.Reviews)
                   .Where(b => b.ID == id)
                   .FirstOrDefault();
              
        }

        public Business GetBusinessByUserID(int id)
        {
            return context.Business
                   .Include(b => b.Address)
                   .Include(b => b.BusinessOwner)
                   .Include(b => b.Category)
                   .Include(b => b.Contacts)
                   .Include(b => b.Images)
                   .Include(b => b.Payment)
                   .Include(b => b.Timings)
                   .Include(c => c.Timings.Monday)
                   .Include(c => c.Timings.Tuesday)
                   .Include(c => c.Timings.Wednesday)
                   .Include(c => c.Timings.Thursday)
                   .Include(c => c.Timings.Friday)
                   .Include(c => c.Timings.Saturday)
                   .Include(c => c.Timings.Sunday)
                   .Include(b => b.Reviews)
                   .Where(b => b.BusinessOwner.ID == id)
                   .FirstOrDefault();

        }

        public Category GetCategoryByCategoryName(string categoryName)
        {
            return context.Category.Where(c => c.CategoryName == categoryName).FirstOrDefault();
        }

        public bool UpdateBusiness(string key, string value, int userId)
        {
            bool isUpdated = false;
           
            if(key == "BusinessName")
            {
                Business business = context.Business.Where(b => b.BusinessOwner.ID == userId).FirstOrDefault();
                business.BusinessName = value;
                isUpdated = context.SaveChanges() == 1? true : false;
            }
            else if (key == "MobileNumber")
            {
                User user = context.Users.Where(u => u.ID == userId).FirstOrDefault();
                user.MobileNumber = value;
                isUpdated =  context.SaveChanges() == 1 ? true : false;
            }
            else if (key == "WhatsAppNumber")
            {
                User user = context.Users.Where(u => u.ID == userId).FirstOrDefault();
                user.WhatsAppNumber = value;
                isUpdated = context.SaveChanges() == 1 ? true : false;
            }
            else if (key == "About")
            {
                Business business = context.Business.Where(b => b.BusinessOwner.ID == userId).FirstOrDefault();
                business.About = value;
                isUpdated = context.SaveChanges() == 1 ? true : false;
            }
            else if (key == "Products")
            {
                Business business = context.Business.Where(b => b.BusinessOwner.ID == userId).FirstOrDefault();
                business.Products = value;
                isUpdated = context.SaveChanges() == 1 ? true : false;
            }
            else if (key == "WebsiteLink")
            {
                Business business = context.Business.Where(b => b.BusinessOwner.ID == userId).FirstOrDefault();
                business.WebsiteLink = value;
                isUpdated = context.SaveChanges() == 1 ? true : false;
            }
            else if (key == "Category")
            {
                Business business = context.Business.Include(c => c.Category).Where(b => b.BusinessOwner.ID == userId).FirstOrDefault();
                Category category = GetCategoryByCategoryName(value);
                business.Category = category;
                isUpdated = context.SaveChanges() == 1 ? true : false;
            }
            else if (key == "Payment")
            {
                Business business = context.Business.Where(b => b.BusinessOwner.ID == userId).FirstOrDefault();
                if(value == "INPROGRESS")
                {
                    business.Payment.PaymentStatus = PaymentStatus.INPROGRESS;
                }
                if (value == "SUCCESSFULL")
                {
                    business.Payment.PaymentStatus = PaymentStatus.SUCCESSFULL;
                }
                if (value == "FAILED")
                {
                    business.Payment.PaymentStatus = PaymentStatus.FAILED;
                }
                
                isUpdated = context.SaveChanges() == 1 ? true : false;
            }
            else if (key == "PaymentID")
            {
                Business business = context.Business.Where(b => b.BusinessOwner.ID == userId).FirstOrDefault();
                business.Payment.TransactionNumber = value;
                isUpdated = context.SaveChanges() == 1 ? true : false;
            }
            else if(key == "DeleteImage")
            {
                Business business = context.Business.Include(c => c.Images).Where(b => b.BusinessOwner.ID == userId).FirstOrDefault();
                List<FilePath> images = business.Images;
                bool found = false;
                for(int i=0;i<images.Count;i++)
                {
                    if(images[i].Path == value)
                    {
                        found = true;                        
                    }
                    if(found == true)
                    {   
                        if(i == 6 || (i == (images.Count-1)))
                        {
                            images.RemoveAt(i);
                        }else
                        images[i].Path = images[i + 1].Path;
                    }
                }
                isUpdated = context.SaveChanges() == 1 ? true : false;
            }
            else if (key == "UnBlockUser")
            {
                Business business = context.Business.Include(c => c.Images).Where(b => b.BusinessOwner.ID == userId).FirstOrDefault();
                business.IsBlocked = false;
                isUpdated = context.SaveChanges() == 1 ? true : false;
            }
            return isUpdated;
        }

        public bool AddReview(Review review, int userID)
        {
            Business business = context.Business.Where(b => b.BusinessOwner.ID == userID).FirstOrDefault();
            Review newReview = new Review();
            newReview = review;
            List<Review> reviews = business.Reviews;
            if(reviews == null)
            {
                reviews = new List<Review>();
            }
            reviews.Add(newReview);
            business.Reviews = reviews;
            return context.SaveChanges() == 1 ? true : false;
        }

        public List<Business> GetAllBusinessByType(string type)
        {
            if (type == "ALL")
            {
                return context.Business
                    .Include(b => b.Address)
                    .Include(b => b.BusinessOwner)
                    .Include(b => b.Category)
                    .Include(b => b.Contacts)
                    .Include(b => b.Images)
                    .Include(b => b.Payment)
                    .Include(b => b.Timings)
                    .ToList();
            }else
            if (type == "PREMIUM")
            {
                return context.Business
                    .Include(b => b.Address)
                    .Include(b => b.BusinessOwner)
                    .Include(b => b.Category)
                    .Include(b => b.Contacts)
                    .Include(b => b.Images)
                    .Include(b => b.Payment)
                    .Include(b => b.Timings)
                    .Where(b => b.Membership == MembershipType.PREMIUM).ToList();
            }
            else if(type == "TOPRATED")
            {
                
                  List<Business> businessList =   context.Business
                    .Include(b => b.Address)
                    .Include(b => b.BusinessOwner)
                    .Include(b => b.Category)
                    .Include(b => b.Contacts)
                    .Include(b => b.Images)
                    .Include(b => b.Payment)
                    .Include(b => b.Timings)
                    .ToList();

                if (businessList.Where(b => b.Reviews != null).Count() > 0)
                {
                   return businessList.OrderByDescending(b => b.OverallRating).ToList();
                }
                else
                {
                    return businessList.OrderByDescending(b => b.RegisteredOn).ToList();
                }

            }
            else if (type == "BLOCKED")
            {
                return context.Business
                    .Include(b => b.Address)
                    .Include(b => b.BusinessOwner)
                    .Include(b => b.Category)
                    .Include(b => b.Contacts)
                    .Include(b => b.Images)
                    .Include(b => b.Payment)
                    .Include(b => b.Timings)
                    .Where(b => b.IsBlocked == true)
                    .ToList();
            }else 
                return null;
        }
        public bool UpdateBusinessLocation(int userId, Location location)
        {
            //Location address = context.Business.Where(l => l.BusinessOwner.ID == userId).FirstOrDefault().Address;
            Business business = context.Business.Include(b => b.Address).Where(l => l.BusinessOwner.ID == userId).FirstOrDefault();
            Location address = business.Address;
            address.AddressLine1 = location.AddressLine1;
            address.Area = location.Area;
            address.City = location.City;
            address.Country = location.Country;
            address.PinCode = location.PinCode;
            address.State = location.State;
            address.Lattitude = location.Lattitude;
            address.Longitude = location.Longitude;
            return context.SaveChanges() == 1 ? true : false;
        }
        public bool AddNewImages(int userId, List<FilePath> images)
        {
            Business business = context.Business.Include(b => b.Images).Where(l => l.BusinessOwner.ID == userId).FirstOrDefault();
            List<FilePath> filePaths = business.Images;
            int x = 0;
            for(int i= business.Images.Count; i<=7; i++)
            {
                if(images.Count > x)
                {
                    filePaths.Add(images[x]);
                }
                x++;
            }
            return context.SaveChanges() == 1 ? true : false;
        }
        public bool UpdateBusinessTimings(int userId, Timings newtimings)
        {
            Business business = context.Business
                                .Include(b => b.Timings)
                                .Include(c => c.Timings.Monday)
                                .Include(c => c.Timings.Tuesday)
                                .Include(c => c.Timings.Wednesday)
                                .Include(c => c.Timings.Thursday)
                                .Include(c => c.Timings.Friday)
                                .Include(c => c.Timings.Saturday)
                                .Include(c => c.Timings.Sunday)
                                .Where(l => l.BusinessOwner.ID == userId)
                                .FirstOrDefault();
            Timings timings = business.Timings;
            timings.Monday.StartTime = newtimings.Monday.StartTime;
            timings.Monday.EndTime = newtimings.Monday.EndTime;
            timings.Monday.Closed = newtimings.Monday.Closed;
            timings.Tuesday.StartTime = newtimings.Tuesday.StartTime;
            timings.Tuesday.EndTime = newtimings.Tuesday.EndTime;
            timings.Tuesday.Closed = newtimings.Tuesday.Closed;
            timings.Wednesday.StartTime = newtimings.Wednesday.StartTime;
            timings.Wednesday.EndTime = newtimings.Wednesday.EndTime;
            timings.Wednesday.Closed = newtimings.Wednesday.Closed;
            timings.Thursday.StartTime = newtimings.Thursday.StartTime;
            timings.Thursday.EndTime = newtimings.Thursday.EndTime;
            timings.Thursday.Closed = newtimings.Thursday.Closed;
            timings.Friday.StartTime = newtimings.Friday.StartTime;
            timings.Friday.EndTime = newtimings.Friday.EndTime;
            timings.Friday.Closed = newtimings.Friday.Closed;
            timings.Saturday.StartTime = newtimings.Saturday.StartTime;
            timings.Saturday.EndTime = newtimings.Saturday.EndTime;
            timings.Saturday.Closed = newtimings.Saturday.Closed;
            timings.Sunday.StartTime = newtimings.Sunday.StartTime;
            timings.Sunday.EndTime = newtimings.Sunday.EndTime;
            timings.Sunday.Closed = newtimings.Sunday.Closed;

            return context.SaveChanges() == 1 ? true : false;
        }

        public bool BlockUser(string blockUser, int userID)
        {
            Business business = context.Business.Where(l => l.BusinessOwner.ID == userID).FirstOrDefault();
            business.IsBlocked = true;
            business.BlockReason = blockUser;
            return context.SaveChanges() == 1 ? true : false;
        }

        public bool UpdatePayment(Business updatedBusiness)
        {
            Business business = context.Business.Where(l => l.BusinessOwner.ID == updatedBusiness.BusinessOwner.ID).FirstOrDefault();
            business = updatedBusiness;
            return context.SaveChanges() == 1 ? true : false;
        }
    }
}
