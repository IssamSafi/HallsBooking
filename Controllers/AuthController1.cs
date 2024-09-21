using Firstproject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Firstproject.Controllers
{
    public class AuthController1 : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthController1(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register([Bind("Id,Fname,Lname,Email,Phone,Address,Imagepath,ImageFile")] User user, string userName, string password)

        {
            if (ModelState.IsValid)
            {
                if (user.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        user.ImageFile.CopyTo(fileStream);
                    }
                    user.Imagepath = fileName;
                }
                _context.Add(user);
                _context.SaveChangesAsync();
                Login usersLogin = new Login();
                usersLogin.UserName = userName;
                usersLogin.Password = password;
                usersLogin.RoleId= 2;
                usersLogin.UserId = user.UserId;
                _context.Add(usersLogin);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Register));
            }
            return View();
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login([Bind("UserName", "Password")] Login usersLogin
)
        {
            var user = _context.Logins.Where(x => x.UserName == usersLogin.UserName && x.Password == usersLogin.Password).SingleOrDefault();
            if (user != null)
            {
                switch (user.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetString("AdminName",user.UserName);   
                        HttpContext.Session.SetInt32("AdminId", (int)user.UserId);
                        return RedirectToAction("Index", "Admincontroller1");

                    case 2:
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetInt32("ClientId", (int)user.UserId);
                        return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "incorrect user name or password");
            return View();
        }
    }

}
