using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firstproject.Models;
using MimeKit;

using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;

namespace Firstproject.Controllers
{
    public class UserhallsController : Controller
    {
        private readonly ModelContext _context;

        public UserhallsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Userhalls
        public async Task<IActionResult> Index()
        {
            var user = _context.Users.ToList();
            int? id = HttpContext.Session.GetInt32("AdminId");
            var image = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
            ViewBag.img=image.Imagepath;
            ViewBag.Admin= HttpContext.Session.GetInt32("AdminId");
            ViewBag.uName= HttpContext.Session.GetString("AdminName");
            var modelContext = _context.Userhalls.Include(u => u.Hall).Include(u => u.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Userhalls/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userhall = await _context.Userhalls
                .Include(u => u.Hall)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userhall == null)
            {
                return NotFound();
            }

            return View(userhall);
        }


        public async Task sendEmail()
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress(" your reservation", "201810741@std-zuj.edu.jo");
            message.From.Add(from);
            MailboxAddress to = new MailboxAddress("User", "safiesam3@gmail.com");
            message.To.Add(to);
            message.Subject = "About of Reservation";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
            "<p style=\"color:pink\">Reservation Reject </p>" + "Sorry" + "<p>Unfortunately, the reservation is already available </p>";
            message.Body = bodyBuilder.ToMessageBody();
            using (var clinte = new SmtpClient())
            {
                clinte.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                clinte.Authenticate("201810741@std-zuj.edu.jo", "SaFiCo##**00");
                clinte.Send(message);
                clinte.Disconnect(true);
            }
        }


        public async Task sendEmail1()
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress(" your reservation", "201810741@std-zuj.edu.jo");
            message.From.Add(from);
            MailboxAddress to = new MailboxAddress("User", "safiesam3@gmail.com");
            message.To.Add(to);
            message.Subject = "About of Reservation";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
            "<p style=\"color:pink\">Reservation Approve </p>" + "successfule" + "<p> The room has been booked </p>";
            message.Body = bodyBuilder.ToMessageBody();
            using (var clinte = new SmtpClient())
            {
                clinte.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                clinte.Authenticate("201810741@std-zuj.edu.jo", "SaFiCo##**00");
                clinte.Send(message);
                clinte.Disconnect(true);
            }
        }





        public async Task<IActionResult> ApproveUserHall(decimal id)
        {
            var userHall = await _context.Userhalls.FindAsync(id);
            if (id != userHall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userHall.IsApprove = true;
                    _context.Update(userHall);
                    await _context.SaveChangesAsync();
                    await sendEmail1();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserhallExists(userHall.Id))
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

            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> RejectUserHall(decimal id)
        {
            var userHall = await _context.Userhalls.FindAsync(id);

            if (id != userHall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userHall.IsApprove = false;
                    _context.Update(userHall);
                    await _context.SaveChangesAsync();
                    await sendEmail();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserhallExists(userHall.Id))
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

            return RedirectToAction(nameof(Index));
        }











        // GET: Userhalls/Create
        public IActionResult Create()
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallName");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Fname");
            return View();
        }

        // POST: Userhalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,UserId,HallId")] Userhall userhall)
        {
            if (ModelState.IsValid)
            {
                var stratdate = userhall.StartDate;
                _context.Add(userhall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallId", userhall.HallId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userhall.UserId);
            return View(userhall);
        }

        // GET: Userhalls/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userhall = await _context.Userhalls.FindAsync(id);
            if (userhall == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallName", userhall.HallId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Fname", userhall.UserId);
            return View(userhall);
        }

        // POST: Userhalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,StartDate,EndDate,UserId,HallId")] Userhall userhall)
        {
            if (id != userhall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userhall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserhallExists(userhall.Id))
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
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallId", userhall.HallId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userhall.UserId);
            return View(userhall);
        }

        // GET: Userhalls/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userhall = await _context.Userhalls
                .Include(u => u.Hall)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userhall == null)
            {
                return NotFound();
            }

            return View(userhall);
        }

        // POST: Userhalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userhall = await _context.Userhalls.FindAsync(id);
            _context.Userhalls.Remove(userhall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserhallExists(decimal id)
        {
            return _context.Userhalls.Any(e => e.Id == id);
        }
    }
}
