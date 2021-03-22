using System;
using System.Collections.Generic;
using System.Text;
using TownerTown.DataAccess;
using TownerTown.Entities;

namespace TownerTown.Service
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository businessRepo;
        public BusinessService(IBusinessRepository businessRepository)
        {
            businessRepo = businessRepository;
        }
        public Business AddNewBusiness(Business business)
        {
            Business newBusiness = new Business();

            newBusiness.BusinessOwner = new User()
            {
                FirstName = business.BusinessOwner.FirstName,
                LastName = business.BusinessOwner.LastName,
                MobileNumber = business.BusinessOwner.MobileNumber,
                WhatsAppNumber = business.BusinessOwner.WhatsAppNumber,
                UserName = business.BusinessOwner.UserName,
                Password = business.BusinessOwner.Password,
                Email = business.BusinessOwner.Email == null ? null : business.BusinessOwner.Email,
                Role = Role.User
            };
            newBusiness.BusinessName = business.BusinessName;
            newBusiness.TagLine = business.TagLine;
            newBusiness.About = business.About;
            newBusiness.Address = new Location()
            {
                AddressLine1 = business.Address.AddressLine1,
                AddressLine2 = business.Address.AddressLine2,
                Area = business.Address.Area,
                City = business.Address.City,
                Country = business.Address.Country,
                PinCode = business.Address.PinCode,
                State = business.Address.State,
                Lattitude = business.Address.Lattitude,
                Longitude = business.Address.Longitude
            };
            newBusiness.Category = businessRepo.GetCategoryByCategoryName(business.Category.CategoryName);
            newBusiness.Products = business.Products;
            newBusiness.RegisteredOn = DateTime.Now.Date;
            newBusiness.RegistrationNumber = business.RegistrationNumber;
            newBusiness.TelephoneNumber = business.TelephoneNumber;
            newBusiness.WebsiteLink = business.WebsiteLink;
            newBusiness.Timings = new Timings()
            {
                Monday = business.Timings.Monday,
                Tuesday = business.Timings.Tuesday,
                Wednesday = business.Timings.Wednesday,
                Thursday = business.Timings.Thursday,
                Friday = business.Timings.Friday,
                Saturday = business.Timings.Saturday,
                Sunday = business.Timings.Sunday
            };
            newBusiness.BusinessID = GenerateBusinessID(business);
            newBusiness.Membership = business.Membership;

            if (business.Contacts != null)
            {
                List<Contact> newcontacts = new List<Contact>();
                foreach (var contact in business.Contacts)
                {
                    Contact newContact = new Contact() { Name = contact.Name, MobileNumber = contact.MobileNumber };
                    newcontacts.Add(newContact);
                }
                newBusiness.Contacts = newcontacts;
            }

            if (business.Images != null)
            {
                List<FilePath> newImages = new List<FilePath>();
                foreach (var image in business.Images)
                {
                    FilePath filePath = new FilePath() { Path = image.Path };
                    newImages.Add(filePath);
                }
                newBusiness.Images = newImages;
            }
            if (business.Membership == MembershipType.PREMIUM)
            {
                if (business.Payment != null)
                {
                    Payment payment = new Payment()
                    {
                        TransactionNumber = business.Payment.TransactionNumber,
                        PaymentStatus = business.Payment.PaymentStatus,
                        PayedOn = DateTime.Today,
                        Amount = business.Payment.Amount,
                        address  = business.Payment.address,
                        contactNumber = business.Payment.contactNumber,
                        currency = business.Payment.currency,
                        description = business.Payment.description,
                        email = business.Payment.email,
                        name = business.Payment.name,
                        orderId = business.Payment.orderId,
                        razorpayKey = business.Payment.razorpayKey
                    };
                    newBusiness.Payment = payment;
                }
            }
            //BusinessRepository repo = new BusinessRepository();
            //return repo.AddBusiness(newBusiness);
            return businessRepo.AddBusiness(newBusiness);

        }

        public List<string> GetALLCities()
        {
            return businessRepo.GetAllCities();
        }

        public List<Business> GetAllReleventBusiness(string location, string category)
        {
            return businessRepo.GetAllReleventBusiness(location, category);
        }

        public List<Category> GetAllCategories()
        {
            return businessRepo.GetAllCategories();
        }

        private string GenerateBusinessID(Business business)
        {
            //BusinessRepository repo = new BusinessRepository();
            string businessID = "YD" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString();
            int count = businessRepo.GetRegisteredBusinessCount() + 1;
            if (count > 0 || count < 10)
            {
                businessID += "000" + count.ToString();
            }
            else
            if (count >= 10 || count < 100)
            {
                businessID += "00" + count.ToString();
            }
            else
            if (count >= 100 || count < 1000)
            {
                businessID += "0" + count.ToString();
            }
            else
            if (count >= 1000)
            {
                businessID += count.ToString();
            }
            return businessID;
        }

        public Business GetBusinessByUserID(int id)
        {
            return businessRepo.GetBusinessByUserID(id);
        }

        public Business GetBusinessByID(int id)
        {
            return businessRepo.GetBusinessByID(id);
        }
        public List<Business> GetAllBusinessByType(string type)
        {
            return businessRepo.GetAllBusinessByType(type);
        }
        public bool UpdateBusiness(string key, string value, int userId)
        {
            return businessRepo.UpdateBusiness(key, value, userId);
        }
        public bool AddNewImages(int userId, List<FilePath> images)
        {
            return businessRepo.AddNewImages(userId, images);
        }

        public bool UpdateBusinessLocation(int userId, Location location)
        {
            return businessRepo.UpdateBusinessLocation(userId, location);
        }

        public bool UpdateBusinessTimings(int userId, Timings newtimings)
        {
            return businessRepo.UpdateBusinessTimings(userId, newtimings);
        }

        public bool AddReview(Review review, int userID)
        {
            return businessRepo.AddReview(review, userID);
        }

        public bool BlockUser(string blockUser, int userID)
        {
            return businessRepo.BlockUser(blockUser, userID);

        }

        public bool UpdatePayment(Business updatedBusiness)
        {
            return businessRepo.UpdatePayment(updatedBusiness);
        }
    }

}
