using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TownerTown.Entities;
using TownerTown.Web.Models;
using TownerTown.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using System.IO;
using System.IO.MemoryMappedFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;

namespace TownerTown.Web.Controllers
{
    //public System.Web.HttpRequestBase Request { get; }
    public class ManageBusinessController : Controller 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment hostingEnvironment;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        
        private IBusinessService _businessService;

        public ManageBusinessController(IBusinessService businessService,IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _httpContextAccessor = httpContextAccessor;
            _businessService = businessService;
            hostingEnvironment = environment;

        }

       
        [HttpGet]
        public IActionResult Index()
        {
            List<Business> businessList = _businessService.GetAllReleventBusiness("Badami", "Clinics");
            List<string> Cities = new List<string>();
            Cities = _businessService.GetALLCities();
            ViewData["items"] = Cities
            .Select(c => new SelectListItem() { Text = c, Value = c })
            .ToList();
            return View(businessList);
        }

        [HttpPost]
        public IActionResult LandingPage(SearchViewModel model)
        {
            BusinessViewModel businessViewModel = new BusinessViewModel();

            if (model == null)
            {
                ViewBag.Message = "Search does not contain any results. Please provide location and search input";
            }
            else if(model.selectedCity == null)
            {
                ViewBag.Message = "Search does not contain any results. Please provide the proper city or select in the dropdown ";
            }
            else if (model.selectedCategories[0] == null)
            {
                ViewBag.Message = "City does not contain the selected search result, please search something else";
            }
            else
            {
                List<Business> businessList = new List<Business>();
                businessList = _businessService.GetAllReleventBusiness(model.selectedCity, model.selectedCategories[0]);
                businessViewModel.Businesses = businessList.ToList();

            }

            List<string> Cities = new List<string>();
            Cities = _businessService.GetALLCities();
            List<City> cities = new List<City>();
            int i = 0;
            foreach (var city in Cities)
            {
                City newCity = new City { CityID = city, CityName = city };
                cities.Add(newCity);
                i++;
            }
            model.Cities = cities;

            List<Category> categories = new List<Category>();
            categories = _businessService.GetAllCategories();
            model.Categories = categories;

            businessViewModel.SearchViewModel = model;

            return View("Index", businessViewModel);
        }

        [HttpGet]
        public IActionResult LandingPageReload(BusinessViewModel businessModel)
        {
            SearchViewModel model = new SearchViewModel();
            List<Business> businessList = new List<Business>();
            businessList = _businessService.GetAllReleventBusiness(businessModel.SearchViewModel.selectedCity, businessModel.SearchViewModel.selectedCategories[0]);
            BusinessViewModel businessViewModel = new BusinessViewModel();
            businessViewModel.Businesses = businessList;

            List<string> Cities = new List<string>();
            Cities = _businessService.GetALLCities();
            List<City> cities = new List<City>();
            int i = 0;
            foreach (var city in Cities)
            {
                City newCity = new City { CityID = city, CityName = city };
                cities.Add(newCity);
                i++;
            }
            model.Cities = cities;

            List<Category> categories = new List<Category>();
            categories = _businessService.GetAllCategories();
            model.Categories = categories;

            businessViewModel.SearchViewModel = model;

            return View("Index", businessViewModel);
        }

        public ActionResult ViewBusiness(int id)
        {
            Business business = new Business();
            business = _businessService.GetBusinessByID(id);
            return View(business);
        }


        public IActionResult Register()
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            List<Category> categories = new List<Category>();
            categories = _businessService.GetAllCategories();
            searchViewModel.Categories = categories;

            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.SearchViewModel = searchViewModel;
            return View(registerViewModel);
        }

        

        [HttpPost]
        public IActionResult RegisterFormFilled(RegisterViewModel registerViewModel)
        {
            //registerViewModel.Business.Category.CategoryName = registerViewModel.SearchViewModel.selectedCategory;
            if(ValidateBusiness(registerViewModel.Business))
            {
                if (registerViewModel.uploadImagesFormFile != null)
                {
                    List<FilePath> imagesFilesPath = new List<FilePath>();
                    foreach (var imageFormFile in registerViewModel.uploadImagesFormFile)
                    {
                        var uniqueFileName = GetUniqueFileName(imageFormFile.FileName);
                        var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                        var filePath = Path.Combine(uploads, uniqueFileName);
                        imageFormFile.CopyTo(new FileStream(filePath, FileMode.Create));
                        FilePath newImageFile = new FilePath();
                        //newImageFile.Path = filePath;
                        newImageFile.Path = "~/uploads/" + imageFormFile.FileName;
                        imagesFilesPath.Add(newImageFile);
                    }
                    registerViewModel.Business.Images = imagesFilesPath;

                    //to do : Save uniqueFileName  to your db table   
                }
                //registerViewModel.Business.Timings = new Timings { Monday = new DayToTimeMapping { StartTime = "10:23", EndTime = "9:00", Closed = false} };
                //if (_businessService.AddNewBusiness(registerViewModel.Business) != null)
                ////if (true)
                //{
                    //return Json(new { success = true, responseText = "You have been successfully registered with Search India. Please Log_in to view your profile" });
                    try
                    {
                        Random randomObj = new Random();
                        string transactionId = randomObj.Next(10000000, 100000000).ToString();

                        Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_E2Zmqlh9v2Dj8U", "NPTQwUFJV6R36LW2fPir43pG");
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", 249);  // Amount will in paise
                        options.Add("receipt", transactionId);
                        options.Add("currency", "INR");
                        options.Add("payment_capture", "0"); // 1 - automatic  , 2 - manual
                                                             //options.Add("notes", "-- You can put any notes here --");
                        Razorpay.Api.Order orderResponse = client.Order.Create(options);
                        string orderId = orderResponse["id"].ToString();

                        // Create order model for return on view
                        OrderModel orderModel = new OrderModel
                        {
                            orderId = orderResponse.Attributes["id"],
                            razorpayKey = "rzp_test_E2Zmqlh9v2Dj8U",
                            amount = 249,
                            currency = "INR",
                            name = registerViewModel.Business.BusinessOwner.FirstName + registerViewModel.Business.BusinessOwner.LastName,
                            email = registerViewModel.Business.BusinessOwner.Email,
                            contactNumber = registerViewModel.Business.BusinessOwner.MobileNumber,
                            address = registerViewModel.Business.Address.AddressLine1 + registerViewModel.Business.Address.City,
                            description = "TownerTown Payments"
                        };

                    Payment payment = new Payment();
                    registerViewModel.Business.Payment = payment;
                    registerViewModel.Business.Payment.address = orderModel.name;
                    registerViewModel.Business.Payment.orderId = orderModel.orderId;
                    registerViewModel.Business.Payment.PayedOn = DateTime.Now;
                    registerViewModel.Business.Payment.PaymentStatus = PaymentStatus.INPROGRESS;
                    registerViewModel.Business.Payment.Amount = orderModel.amount;
                    registerViewModel.Business.Payment.contactNumber = orderModel.contactNumber;
                    registerViewModel.Business.Payment.currency = orderModel.currency;
                    registerViewModel.Business.Payment.description = orderModel.description;
                    registerViewModel.Business.Payment.email = orderModel.email;
                    registerViewModel.Business.Payment.name = orderModel.name;
                    registerViewModel.Business.Payment.razorpayKey = orderModel.razorpayKey;
                    registerViewModel.Business.Payment.TransactionNumber = "value";

                    Business business = _businessService.AddNewBusiness(registerViewModel.Business);
                    if (business != null)
                    {
                        _session.SetInt32("UserID", business.BusinessOwner.ID);
                        if(business.Membership == MembershipType.PREMIUM)
                        {
                            return View("Payments", orderModel);
                        }
                        else if(business.Membership == MembershipType.GENERAL)
                        {
                            return RedirectToAction("ViewProfile");
                        }
                        
                    }

                }
                catch (Exception ex)
                    {
                        //throw (e);
                        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                        message += string.Format("<b>StackTrace:</b> {0}<br /><br />", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
                        message += string.Format("<b>Source:</b> {0}<br /><br />", ex.Source.Replace(Environment.NewLine, string.Empty));
                        message += string.Format("<b>TargetSite:</b> {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
                        ModelState.AddModelError(string.Empty, message);
                        ViewBag.Message = message;
                        return View("Register");
                    }
                    
                //}
            }
            else
            {
               return View("Register");
                //return Json(new { success = false, responseText = "OOOpppsss....Something went wrong, the registration cannot be saved at this movement please contact customer support." });
            }
            return View("Register");
            //return Json(new { success = false, responseText = "Validation doesn't match. Please check validation error message for more details" });

        }

        public ActionResult ViewProfile()
        {
            int userId = _session.GetInt32("UserID").Value;
            string role = _session.GetString("Role");
            if (role == Role.Admin)
            {
                return View("AdminProfile");
            }
            RegisterViewModel viewModel = new RegisterViewModel();
            Business business = _businessService.GetBusinessByUserID(userId);
            viewModel.Business = business;
            if(business.Payment.PaymentStatus == PaymentStatus.FAILED || business.Payment.PaymentStatus == PaymentStatus.INPROGRESS)
            {
                ViewBag.Message = "Please make payment to visit your profile";
                //return RedirectToAction("LogIn", "Home");
            }
            SearchViewModel model = new SearchViewModel();
            List<Category> categories = new List<Category>();
            categories = _businessService.GetAllCategories();
            model.Categories = categories;
            viewModel.SearchViewModel = model;
            return View("Profile", viewModel);
        }
        [HttpGet]
        public ActionResult GetAllReleventBusiness(string location, string category)
        {
            //List<Business> businessList = _businessService.GetAllReleventBusiness(location, category);
            //return  PartialView("_AllBusinesses", businessList);
            SearchViewModel model = new SearchViewModel();
            List<Business> businessList = new List<Business>();
            businessList = _businessService.GetAllReleventBusiness(location, category);
            BusinessViewModel businessViewModel = new BusinessViewModel();
            businessViewModel.Businesses = businessList;

            List<string> Cities = new List<string>();
            Cities = _businessService.GetALLCities();
            List<City> cities = new List<City>();
            int i = 0;
            foreach (var city in Cities)
            {
                City newCity = new City { CityID = city, CityName = city };
                cities.Add(newCity);
                i++;
            }
            model.Cities = cities;

            List<Category> categories = new List<Category>();
            categories = _businessService.GetAllCategories();
            model.Categories = categories;

            model.selectedCity = location;
            string[] selectedCategories = { category };
            model.selectedCategories = selectedCategories;
            businessViewModel.SearchViewModel = model;

            return View("Index", businessViewModel);
        }

        [HttpPost]
        public IActionResult UpdateProfile(string key, string value)
        {
            try
            {
                int userId = _session.GetInt32("UserID").Value;
                _businessService.UpdateBusiness(key, value, userId);
                return Json("Updated Successfully. Thank You");
            }
            catch (Exception e)
            {
                return Json("Something went wrong please try later" + e);
            }

        }

        [HttpPost]
        public IActionResult UnBlockUser(int userID)
        {
            try
            {
                string key = "UnBlockUser";
                string value = "false";
                _businessService.UpdateBusiness(key, value, userID);
                return Json("Updated Successfully. Thank You");
            }
            catch (Exception e)
            {
                return Json("Something went wrong please try later" + e);
            }

        }
        [HttpPost]
        public IActionResult UpdateTimings(Timings timings)
        {
            try
            {
                int userId = _session.GetInt32("UserID").Value;
                _businessService.UpdateBusinessTimings(userId, timings);
                return Json("Updated Successfully. Thank You");
            }
            catch (Exception e)
            {
                return Json("Something went wrong please try later" + e);
            }

        }
        [HttpPost]
        public IActionResult UpdateAddress(Location location)
        {
            try
            {
                int userId = _session.GetInt32("UserID").Value;
                _businessService.UpdateBusinessLocation(userId ,location);
                return Json("Updated Successfully. Thank You");
            }
            catch (Exception e)
            {
                return Json("Something went wrong please try later" + e);
            }

        }
        [HttpPost]
        public ActionResult AddImages(RegisterViewModel viewModel)
        {
            if (viewModel.uploadImagesFormFile.Count != 0)
            {
                List<FilePath> imagesFilesPath = new List<FilePath>();
                foreach (var imageFormFile in viewModel.uploadImagesFormFile)
                {
                    var uniqueFileName = GetUniqueFileName(imageFormFile.FileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    imageFormFile.CopyTo(new FileStream(filePath, FileMode.Create));
                    FilePath newImageFile = new FilePath();
                    //newImageFile.Path = filePath;
                    newImageFile.Path = "~/uploads/" + uniqueFileName;
                    imagesFilesPath.Add(newImageFile);
                }
                int userId = _session.GetInt32("UserID").Value;
                _businessService.AddNewImages(userId, imagesFilesPath);
                
                //to do : Save uniqueFileName  to your db table   
            }
            return RedirectToAction("ViewProfile");
        }

        [HttpPost]
        public ActionResult GetCustomersByType(string type)
        {
            List<Business> businessList = _businessService.GetAllBusinessByType(type);
            return PartialView("_businessList", businessList);
        }
        [HttpPost]
        public ActionResult AddReview(Review review, int userID)
        {
            review.ReviewdOn = DateTime.Now;
            //int userId = _session.GetInt32("UserID").Value;
            if(_businessService.AddReview(review, userID))
            {
                return Json(new { success = true, responseText = "Thanks for the review" });
            }else
            {
                return Json(new { success = false, responseText = "Sorry something went wrong. Please try again later" });
            }
           
        }

        [HttpPost]
        public ActionResult BlockUser(string blockReason, int userID)
        {
            
            if (_businessService.BlockUser(blockReason, userID))
            {
                return Json(new { success = true, responseText = "Blocked Successfully" });
            }
            else
            {
                return Json(new { success = false, responseText = "Sorry something went wrong. Please try again later" });
            }

        }
        private bool ValidateBusiness(Business business)
        {
            bool isValidated = true;
            ViewBag.ErrorMessage = "";
            if (business.About == null || business.About == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "About field is mandatory. "; 
            }
            if(business.Address.AddressLine1 == null || business.Address.AddressLine1 == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "Address is mandatory. ";
            }
            if (business.Address.Area == null || business.Address.Area == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "Area is mandatory. ";
            }
            if (business.Address.PinCode == 0)
            {
                isValidated = false;
                ViewBag.ErrorMessage += "PinCode is mandatory. ";
            }
            if (business.BusinessName == null || business.BusinessName == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "BusinessName is mandatory. ";
            }
            if (business.BusinessOwner.FirstName == null || business.BusinessOwner.FirstName == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "FirstName is mandatory. ";
            }
            if (business.BusinessOwner.LastName == null || business.BusinessOwner.LastName == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "LastName is mandatory. ";
            }
            if (business.BusinessOwner.MobileNumber == null || business.BusinessOwner.MobileNumber == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "Mobile Number is mandatory. ";
            }
            if (business.BusinessOwner.WhatsAppNumber == null || business.BusinessOwner.WhatsAppNumber == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "Whatsapp Number is mandatory. ";
            }
            if (business.Category == null)
            {
                isValidated = false;
                ViewBag.ErrorMessage += "Category is mandatory. ";
            }
            //if (business.Images == null)
            //{
            //    isValidated = false;
            //    ViewBag.ErrorMessage += "Images are mandatory. ";
            //}
            if (business.Products == null || business.Products == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "Products field is mandatory. ";
            }
            if (business.TagLine == null || business.TagLine == "")
            {
                isValidated = false;
                ViewBag.ErrorMessage += "TagLine is mandatory. ";
            }
            if (business.Timings == null)
            {
                isValidated = false;
                ViewBag.ErrorMessage += "Timings is mandatory. ";
            }

            return isValidated;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        private OrderModel Payments(RegisterViewModel registerViewModel)
        {
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_E2Zmqlh9v2Dj8U", "NPTQwUFJV6R36LW2fPir43pG");
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", 249);  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); // 1 - automatic  , 2 - manual
                                                 //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();

            // Create order model for return on view
            OrderModel orderModel = new OrderModel
            {
                orderId = orderResponse.Attributes["id"],
                razorpayKey = "rzp_test_E2Zmqlh9v2Dj8U",
                amount = 249,
                currency = "INR",
                name = registerViewModel.Business.BusinessOwner.FirstName + registerViewModel.Business.BusinessOwner.LastName,
                email = registerViewModel.Business.BusinessOwner.Email,
                contactNumber = registerViewModel.Business.BusinessOwner.MobileNumber,
                address = registerViewModel.Business.Address.AddressLine1 + registerViewModel.Business.Address.City,
                description = "TownerTown Payments"
            };

            // Return on PaymentPage with Order data
            return orderModel;
        }

        [HttpPost]
        public ActionResult Complete()
        {
            // Payment data comes in url so we have to get it from url

            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = Request.Form["rzp_paymentid"];
            
            // This is orderId
            string orderId = Request.Form["rzp_orderid"];

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_E2Zmqlh9v2Dj8U", "NPTQwUFJV6R36LW2fPir43pG");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];

            //// Check payment made successfully
            int userId = _session.GetInt32("UserID").Value;
            Business business = _businessService.GetBusinessByUserID(userId);
            business.Payment.TransactionNumber = paymentId;
            _businessService.UpdatePayment(business);
            //_businessService.UpdateBusiness("PaymentID", paymentId, userId);

            if (paymentCaptured.Attributes["status"] == "captured")
            {
                _businessService.UpdateBusiness("Payment", "SUCCESSFULL", userId);
                // Create these action method
                ViewBag.Message = "Payment Successfull.Thanks for registering with TownerTown.";
                return RedirectToAction("ViewProfile");
            }
            else
            {
                _businessService.UpdateBusiness("Payment", "FAILED", userId);
                ViewBag.Message = "Sorry Payment Failed.Please try again later";
                return RedirectToAction("LogIn","Home");
            }
        }

        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Failed()
        {
            return View();
        }

        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
    }
}