using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register-user")]
        public IActionResult Register (User user) {
            if(ModelState.IsValid) {
                if(dbContext.Users.Any(a => a.Email == user.Email)) {
                    ModelState.AddModelError("Email", "Email already exists...");
                    return View("Index");}
                if(dbContext.Users.Any(u => u.UserName == user.UserName)) {
                    ModelState.AddModelError("UserName", "UserName already exists...");
                    return View("Index");
                
                } else {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    dbContext.SaveChanges();
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("logged_user", user.UserId);
                    return RedirectToAction("Dashboard");
                }
                
            } else {
                return View("Index");
            }
        }
        [HttpGet("clear")]
        public IActionResult Clear() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost("login-user")]
        public IActionResult LoginUser(LoginUser logged) {
            var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == logged.Email);
            if(ModelState.IsValid) {
                if(userInDb == null) {
                    ModelState.AddModelError("Email", "Invalid email/password!");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(logged, userInDb.Password, logged.Password);
                if(result == 0) {
                    ModelState.AddModelError("Password", "Invalid Email/Password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("logged_user", userInDb.UserId);
                return RedirectToAction("Dashboard");
            }
            else {
                return View("Index");
            }

        }
        [HttpGet("Dashboard")]
        public IActionResult Dashboard() {
            ViewModel Dashview = new ViewModel
            {
                AllProducts = dbContext.Products.Include(e => e.CReviews).ThenInclude(u => u.Reviewer).ToList(),
                OneUser =dbContext.Users.FirstOrDefault(us => us.UserId == (int)HttpContext.Session.GetInt32("logged_user"))
            };
            if(HttpContext.Session.GetInt32("logged_user") != null) {
                return View(Dashview);
            } else {
                ModelState.AddModelError("Email", "You are not logged in!");
                return View("Index");
            }
            }
        [HttpGet("NewProd")]
        public IActionResult NewProd() {
            ViewModel Newview = new ViewModel
            {
                OneProduct = new Product()
            };
            return View();
        }
        [HttpPost("createproduct")]
        public IActionResult CreateProduct(ViewModel data){
            if(ModelState.IsValid){
            Product e = new Product
            
            {
                UserId = dbContext.Users.FirstOrDefault(us => us.UserId == (int)HttpContext.Session.GetInt32("logged_user")).UserId,
                ProductName = data.OneProduct.ProductName,
                Category = data.OneProduct.Category,
                Price = data.OneProduct.Price,
                Description = data.OneProduct.Description,
                Image = data.OneProduct.Image,
            };
           
                dbContext.Products.Add(e);
                dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
            } else {
                // ModelState.AddModelError("EventName","What's the big event?");
                // ModelState.AddModelError("Event","What's the big event?");
                // ModelState.AddModelError("Event","What's the big event?");
                // ModelState.AddModelError("Event","What's the big event?");
                // ModelState.AddModelError("Event","What's the big event?");
                // ModelState.AddModelError("Event","What's the big event?");
                // ModelState.AddModelError("Event","What's the big event?");
                return View("NewProd", data);
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
    }
}
