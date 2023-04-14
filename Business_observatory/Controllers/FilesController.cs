using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Business_observatory.Data;
using Business_observatory.Models;
using System.Security.Claims;

namespace Business_observatory.Controllers
{
    public class FilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Files.Include(f => f.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var file = await _context.Files
                .Include(f => f.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RegistrationDate,Format,Route,ProjectId")] Models.File file)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ModelState.IsValid)
            {

                _context.Add(file);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", file.ProjectId);
            return View(file);
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", file.ProjectId);
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RegistrationDate,Format,Route,ProjectId")] Models.File file)
        {
            if (id != file.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(file);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(file.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", file.ProjectId);
            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Files == null)
            {
                return NotFound();
            }

            var file = await _context.Files
                .Include(f => f.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Files == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Files'  is null.");
            }
            var file = await _context.Files.FindAsync(id);
            if (file != null)
            {
                _context.Files.Remove(file);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int id)
        {
          return (_context.Files?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
