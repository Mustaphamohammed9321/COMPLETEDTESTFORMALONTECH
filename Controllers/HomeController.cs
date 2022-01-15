using MALON_GLOBAL_WEBAPP.Helper;
using MALON_GLOBAL_WEBAPP.Interfaces;
using MALON_GLOBAL_WEBAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MALON_GLOBAL_WEBAPP.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        //private static string webAPIClient = "https://https://localhost:44364/";

        private readonly ILogger<HomeController> _logger;
        protected readonly IUserService _userService;
        protected readonly JwtAuthenticationManager _jwtManager;

        public HomeController(ILogger<HomeController> logger, IUserService userService, JwtAuthenticationManager jwtManager)
        {
            _logger = logger;
            _userService = userService;
            _jwtManager = jwtManager;
        }

        //[Authorize]
        public IActionResult Index(UserDetailsDTO user)
        {
            //var token = HttpContext.Session.GetString("Token");

            var token = user.Token;
            var fullName = user.FullName;
            var email = user.Email;

            if(token == String.Empty || token == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Token = token;
                ViewBag.EmailAddress = email;
                ViewBag.FullName = fullName;
                return View();
            }
        } 
        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Login()  //login page
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<ActionResult> Login(UserAccount oldUser)
        {
            if (oldUser == null)
            {
                return ViewBag("invalid login details");
            }
            else
            {
                var user = await _userService.ValidLoginAsync(oldUser.Email, oldUser.Password);
                if (user == null)
                {
                    TempData["Alertmessage"] = "Invalid login details, please try again";
                    //oldUser.AuthenticationErrorMessage = "Invalid login details, please try again";
                    return View();
                }
                else
                {
                    //var token = _jwtManager.GenerateToken(user.Email);
                    //var FullName = $"{user.FirstName}  {user.LastName}";
                    //var Email = user.Email;

                    //TempData["Alertmessage"] = "Login Successful";
                    HttpContext.Session.SetString("Token", _jwtManager.GenerateToken(user.Email));
                    HttpContext.Session.SetString("FullName", $"{user.FirstName}  {user.LastName}");
                    HttpContext.Session.SetString("Email", user.Email);

                    //TempData["Email"] = user.Email;
                    //TempData["FullName"] = $"{user.FirstName}  {user.LastName}";
                    //TempData["Token"] = _jwtManager.GenerateToken(user.Email);

                    //Response.Write("")

                    return RedirectToAction("Index", "Home",
                        new UserDetailsDTO
                        {
                            Email = user.Email,
                            FullName = $"{user.FirstName}  {user.LastName}",
                            Token = _jwtManager.GenerateToken(user.Email)
                        });
                }
            }


        }

        [HttpPost]
        public IActionResult Signup(UserAccount newUser)
        {
            if (ModelState.IsValid)
            {
                var accountCreationResponse = _userService.CreateNewAccountAsync(newUser);
                if (accountCreationResponse == true)
                {
                    TempData["Message"] = "Acccount Created Successfully";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    TempData["Message"] = "Could not create account at this time, please try again";
                    return View();
                }
            }
            else
            {
                TempData["Message"] = "Invalid request encountered";
                return View();
            }
        }

        public class UserDetailsDTO
        {
            public string Email { get; set; }
            public string FullName { get; set; }
            public string Token { get; set; }
        }


    }
}
