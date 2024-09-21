using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firstproject.Models;
using Microsoft.AspNetCore.Http;

namespace Firstproject.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ModelContext _context;

        public ContactUsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ContactUs
        public async Task<IActionResult> Index()

        {
            var user = _context.Users.ToList();
            int? id = HttpContext.Session.GetInt32("AdminId");
            var image = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
            ViewBag.img=image.Imagepath;
            ViewBag.Admin= HttpContext.Session.GetInt32("AdminId");
            ViewBag.uName= HttpContext.Session.GetString("AdminName");
            return View(await _context.ContactUs.ToListAsync());
        }

        // GET: ContactUs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.ConId == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConId,Location,Telephone,Email,Name,YEmail,Subject,Message")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactU);
        }

        // GET: ContactUs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs.FindAsync(id);
            if (contactU == null)
            {
                return NotFound();
            }
            return View(contactU);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ConId,Location,Telephone,Email,Name,YEmail,Subject,Message")] ContactU contactU)
        {
            if (id != contactU.ConId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUExists(contactU.ConId))
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
            return View(contactU);
        }

        // GET: ContactUs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.ConId == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactU = await _context.ContactUs.FindAsync(id);
            _context.ContactUs.Remove(contactU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUExists(decimal id)
        {
            return _context.ContactUs.Any(e => e.ConId == id);
        }
    }
}
