using Firstproject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Firstproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ModelContext _context;
        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment=webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var about = _context.AboutUs.ToList().FirstOrDefault();

            var home = _context.Homes.ToList().FirstOrDefault();
            var contact = _context.ContactUs.ToList().FirstOrDefault();
            //ViewBag.About = about.Image;
            ViewBag.cliant= HttpContext.Session.GetInt32("ClientId");
            ViewBag.Uname= HttpContext.Session.GetString("UserName");
            ViewBag.cont= contact.Location;
            ViewBag.tel= contact.Telephone;
            ViewBag.em= contact.Email;
            ViewBag.Home = home.Image1;
            ViewBag.HomeImage = home.Image2;
            ViewBag.HomeImage2 = home.Image3;
            ViewBag.HomeT= home.Title;
            var index = await _context.Userhalls.Include(x => x.User).Include(x => x.Hall).ToListAsync();
            var card = await _context.Halls.Include(x => x.Address).ToListAsync();
            return View(card);
        }


        public async Task<IActionResult> updateU(decimal? id)
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
        public async Task<IActionResult> updateU(decimal id, [Bind("UserId,Fname,Lname,Phone,Email,Imagepath,ImageFile,Address")] User user)
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











        public IActionResult Booking()
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallName");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Fname");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Booking([Bind("Id,StartDate,EndDate,UserId,HallId")] Userhall userhall)
        {
            if (ModelState.IsValid)
            {
                var stratdate = userhall.StartDate;
                _context.Add(userhall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Booking));
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallId", userhall.HallId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userhall.UserId);
            return View(userhall);
        }



        public IActionResult Contact()
        {
            var contact = _context.ContactUs.ToList().FirstOrDefault();
            var home = _context.Homes.ToList().FirstOrDefault();
            ViewBag.cliant= HttpContext.Session.GetInt32("ClientId");
            ViewBag.Uname= HttpContext.Session.GetString("UserName");
            ViewBag.HomeImage= home.Image3;
            ViewBag.cont= contact.Location;
            ViewBag.tel= contact.Telephone;
            ViewBag.em= contact.Email;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("ConId,Location,Telephone,Email,Name,YEmail,Subject,Message")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Contact));
            }
            return View(contactU);
        }








       
        



        





































        public IActionResult About()
        {
            var contact = _context.ContactUs.ToList().FirstOrDefault();

            var about = _context.AboutUs.ToList().FirstOrDefault();
            var home = _context.Homes.ToList().FirstOrDefault();
            ViewBag.cliant= HttpContext.Session.GetInt32("ClientId");
            ViewBag.Uname= HttpContext.Session.GetString("UserName");
            ViewBag.Home = home.Image1;
            ViewBag.cont= contact.Location;
            ViewBag.tel= contact.Telephone;
            ViewBag.em= contact.Email;

            ViewBag.About = about.Image;
            ViewBag.AboutD= about.Description;
            return View();
        }

       




        public async Task<IActionResult> HallsVAsync()
        {
            var contact = _context.ContactUs.ToList().FirstOrDefault();

            var hallv = await _context.Halls.Include(x => x.Address).ToListAsync();
            ViewBag.cliant= HttpContext.Session.GetInt32("ClientId");
            ViewBag.Uname= HttpContext.Session.GetString("UserName");
            ViewBag.cont= contact.Location;
            ViewBag.tel= contact.Telephone;
            ViewBag.em= contact.Email;
            return View(hallv);
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
