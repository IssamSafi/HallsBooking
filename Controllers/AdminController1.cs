using Firstproject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Firstproject.Controllers
{
    public class AdminController1 : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController1(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Index()
        {

            ViewBag.User = _context.Users.Count();
            //ViewBag.TAdmin= _context.Logins.Count(x => x.RoleId==1);
            //ViewBag.Tusers= _context.Logins.Count(x => x.RoleId==2);
            var username = _context.Users.Select(x => x.Fname).ToList();
            var res = _context.Userhalls.Include(x => x.User).Include(h => h.Hall);
            List<int> ch = new List<int>();
            foreach(var item in username)
            {
                ch.Add(res.Count(x => x.User.Fname == item));
            }
            ViewBag.us= username;
            ViewBag.ch= ch;
            ViewBag.search=_context.Users.ToList();
            ViewData["esam"]=_context.Users.Count();
            var user = _context.Users.ToList();
            int? id = HttpContext.Session.GetInt32("AdminId");
            var image= _context.Users.Where(x=> x.UserId == id).FirstOrDefault(); 
            ViewBag.img=image.Imagepath;  
            ViewBag.Admin= HttpContext.Session.GetInt32("AdminId");
            ViewBag.uName= HttpContext.Session.GetString("AdminName");
            var users = _context.Users.ToList();
            var halls = _context.Halls.ToList();
            var userhalls = _context.Userhalls.ToList();
            var addresses = _context.Addresses.ToList();

            var multiTable = from uh  in userhalls
                             join u in users on uh.UserId equals u.UserId
                             join h in halls on uh.HallId equals h.HallId
                             join ad in addresses on h.AddressId equals ad.AddressId
                             select new JoinTable { Hall = h, Address = ad, User = u, Userhall=uh };


            

            
            return View(multiTable);

            
        }
       
        public async Task<IActionResult> update(decimal? id)
        {

          
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> update(decimal id, [Bind("UserId,Fname,Lname,Phone,Email,Imagepath,ImageFile,Address")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.ImageFile != null)
                    {
                        string wwwrootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;

                        string path = Path.Combine(wwwrootPath + "/Image/" + fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await user.ImageFile.CopyToAsync(filestream);
                        }
                        user.Imagepath = fileName;
                    }




                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);

        }
        private bool UserExists(decimal id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
        public IActionResult JoinTable()
        {
            var users = _context.Users.ToList();
            var halls = _context.Halls.ToList();
            var userhalls= _context.Userhalls.ToList();
            var addresses = _context.Addresses.ToList();

            var multiTable = from uh in userhalls
                             join u in users on uh.UserId equals u.UserId
                             join h in halls on uh.HallId equals h.HallId
                             join ad in addresses on h.AddressId equals ad.AddressId
                             select new JoinTable { Hall = h, Address = ad, User = u, Userhall=uh };

            return View(multiTable);
        }

        [HttpGet]
        public IActionResult Report()
        {
            ViewData["hallPrice"] = _context.Halls.Sum(x => x.HallPrice);

            var users = _context.Users.ToList();
            var halls = _context.Halls.ToList();
            var userhalls = _context.Userhalls.ToList();
            var addresses = _context.Addresses.ToList();
            var multiTable = from uh in userhalls
                             join u in users on uh.UserId equals u.UserId
                             join h in halls on uh.HallId equals h.HallId
                             join ad in addresses on h.AddressId equals ad.AddressId
                             select new JoinTable { Hall = h, Address = ad, User = u, Userhall=uh };

            

            
            return View(multiTable);


           
        }
        [HttpPost]
        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            var users = _context.Users.ToList();
            var halls = _context.Halls.ToList();
            var userhalls = _context.Userhalls.ToList();
            var addresses = _context.Addresses.ToList();
            var multiTable = from uh in userhalls
                             join u in users on uh.UserId equals u.UserId
                             join h in halls on uh.HallId equals h.HallId
                             join ad in addresses on h.AddressId equals ad.AddressId
                             select new JoinTable { Hall = h, Address = ad, User = u, Userhall=uh };

            

            if (startDate == null && endDate == null)
            {

                
                return View(multiTable);
            }
            else if (startDate == null && endDate != null)
            {

                var result = multiTable.Where(x => x.Userhall.EndDate.Value.Date == endDate).ToList();

                return View(result);
            }
            else if (startDate != null && endDate == null)
            {

                var result = multiTable.Where(x => x.Userhall.StartDate.Value.Date == startDate).ToList();

                return View(result);


            }
            else
            {

                var result = multiTable.Where(x => x.Userhall.EndDate.Value.Date>= startDate && x.Userhall.StartDate.Value.Date <= endDate).ToList();

                return View(result);


            }
        }

    }
}
