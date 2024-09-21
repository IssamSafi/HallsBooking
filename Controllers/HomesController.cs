using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firstproject.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Firstproject.Controllers
{
    public class HomesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Homes
        public async Task<IActionResult> Index()
        {
            
            var home = _context.Homes.ToList().FirstOrDefault();

            //ViewBag.Home = home.Image1;
            //ViewBag.HomeImage = home.Image2;
            ////ViewBag.HomeImage2 = home.Image3;
            return View(await _context.Homes.ToListAsync());
            
        }

        // GET: Homes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: Homes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image1,Image2,Image3,Title,ImageFile,ImageFile1,ImageFile2")] Home home)
        {
            if (ModelState.IsValid)
            {
                if (home.ImageFile !=null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.ImageFile.CopyToAsync(fileStream);
                    }
                    home.Image1 = fileName;
                }

                if (home.ImageFile1 !=null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + home.ImageFile1.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.ImageFile1.CopyToAsync(fileStream);
                    }
                    home.Image2 = fileName;
                }
                if (home.ImageFile2 !=null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + home.ImageFile2.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await home.ImageFile2.CopyToAsync(fileStream);
                    }
                    home.Image3 = fileName;
                }



                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        // GET: Homes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            return View(home);
        }

        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Image1,Image2,Image3,Title,ImageFile,ImageFile1,ImageFile2")] Home home)
        {
            if (id != home.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                
                try
                {
                    if (home.ImageFile !=null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.ImageFile.CopyToAsync(fileStream);
                        }
                        home.Image1 = fileName;
                    }

                    if (home.ImageFile1 !=null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName1 = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName1);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.ImageFile.CopyToAsync(fileStream);
                        }
                        home.Image1 = fileName1;
                    }
                    if (home.ImageFile2 !=null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName2 = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName2);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await home.ImageFile.CopyToAsync(fileStream);
                        }
                        home.Image1 = fileName2;
                    }



                    _context.Update(home);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Id))
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
            return View(home);
        }

        // GET: Homes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var home = await _context.Homes.FindAsync(id);
            _context.Homes.Remove(home);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(decimal id)
        {
            return _context.Homes.Any(e => e.Id == id);
        }
    }
}
