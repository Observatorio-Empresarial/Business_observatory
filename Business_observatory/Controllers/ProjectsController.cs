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
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects.Include(p => p.ApplicationUser).Include(p => p.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ApplicationUser)
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public async Task<IActionResult> Create()
        {
            var model = new ProjectCreate()
            {
                Categories=await _context.Categories.ToListAsync(),
                Project = new Project()
            };
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            return View(model);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,RegistrationDate,Status,CompanyId,AspNetUserId")] Project project,int CategoryID)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ModelState.IsValid)
            {
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    project.AspNetUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    _context.Add(project);
                    await _context.SaveChangesAsync();

                    var newCatPro = new Categoriesproject()
                    {
                        CategoryId = CategoryID,
                        ProjectId = project.Id
                    };
                    _context.Add(newCatPro);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Manejar la excepción
                }
            }
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", project.AspNetUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", project.CompanyId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", project.AspNetUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", project.CompanyId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,RegistrationDate,Status,CompanyId,AspNetUserId")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", project.AspNetUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", project.CompanyId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ApplicationUser)
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
