using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Business_observatory.Models;
=======
using Business_observatory.Data;
using Business_observatory.Models;
using System.ComponentModel;
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190

namespace Business_observatory.Controllers
{
    public class ProjectsController : Controller
    {
<<<<<<< HEAD
        private readonly ObservatorioEmpresarialContext _context;

        public ProjectsController(ObservatorioEmpresarialContext context)
=======
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
<<<<<<< HEAD
              return _context.Projects != null ? 
                          View(await _context.Projects.ToListAsync()) :
                          Problem("Entity set 'ObservatorioEmpresarialContext.Projects'  is null.");
=======
            var applicationDbContext = _context.Projects.Include(p => p.ApplicationUser).Include(p => p.Company);
            return View(await applicationDbContext.ToListAsync());
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
<<<<<<< HEAD
                .FirstOrDefaultAsync(m => m.IdProject == id);
=======
                .Include(p => p.ApplicationUser)
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
<<<<<<< HEAD
        public IActionResult Create()
        {
            return View();
=======
        public async Task<IActionResult> Create()
        {
            var selectCategories=await _context.Categories.ToListAsync();

            //ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            return View(selectCategories);
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public async Task<IActionResult> Create([Bind("IdProject,Title,Description,File,CreationDate,UpdateDate,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
=======
        public async Task<IActionResult> Create([Bind("Id,Title,Description,RegistrationDate,Status,CompanyId,AspNetUserId")] Project project,[Bind("Categories")] Categoriesproject categoriesproject)
        {
            if (ModelState.IsValid)
            {
                var categoryId = ViewData["Categories"];
                ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Id");
                var newCatPro = new Categoriesproject()
                {
                    CategoryId = categoriesproject.CategoryId,
                    ProjectId = project.Id,
                 };
                _context.Add(categoriesproject);
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
<<<<<<< HEAD
=======
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", project.AspNetUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", project.CompanyId);
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
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
<<<<<<< HEAD
=======
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", project.AspNetUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", project.CompanyId);
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public async Task<IActionResult> Edit(int id, [Bind("IdProject,Title,Description,File,CreationDate,UpdateDate,Status")] Project project)
        {
            if (id != project.IdProject)
=======
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,RegistrationDate,Status,CompanyId,AspNetUserId")] Project project)
        {
            if (id != project.Id)
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
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
<<<<<<< HEAD
                    if (!ProjectExists(project.IdProject))
=======
                    if (!ProjectExists(project.Id))
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
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
<<<<<<< HEAD
=======
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", project.AspNetUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", project.CompanyId);
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
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
<<<<<<< HEAD
                .FirstOrDefaultAsync(m => m.IdProject == id);
=======
                .Include(p => p.ApplicationUser)
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
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
<<<<<<< HEAD
                return Problem("Entity set 'ObservatorioEmpresarialContext.Projects'  is null.");
=======
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
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
<<<<<<< HEAD
          return (_context.Projects?.Any(e => e.IdProject == id)).GetValueOrDefault();
=======
          return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
>>>>>>> 2cbae7c4a88c311a52d3d0b4b4c4d1b6372ec190
        }
    }
}
