using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TownerTown.Entities;
using TownerTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TownerTown.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private IUserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public AuthorizationController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }


        [AllowAnonymous]
        public IActionResult Authenticate([FromForm] string Username, [FromForm] string Password)
        {
            var user = _userService.Authenticate(Username, Password);

            if (user == null)
            {
                if (Username == "" && Password == "")
                {
                    TempData["Message"] = "FirstTimeLogIn";
                }
                else
                {
                    TempData["Message"] = "Error";
                }

                return RedirectToAction("LogIn", "Home");
            }

            _session.SetString("JWToken", user.Token);
            _session.SetInt32("UserID", user.ID);
            _session.SetString("Role", user.Role);
            return RedirectToAction("ViewProfile", "ManageBusiness");
            // return Ok(user);
        }



        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}