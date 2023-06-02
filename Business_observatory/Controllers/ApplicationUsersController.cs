using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Business_observatory.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.ApplicationUser != null ?
                        View(await _context.ApplicationUser.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.ApplicationRole'  is null.");
        }

        // GET: ApplicationRoles/Create
        public async Task<IActionResult> Create()
        {
            var model = new UserRoles()
            {
                applicationRoles = await _context.ApplicationRole.ToListAsync(),
                applicationUser = new ApplicationUser()
            };
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            ViewData["RoleId"] = new SelectList(_context.ApplicationRole, "Id", "Id");
            return View(model);
        }

        // POST: ApplicationRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Email,PhoneNumber,PasswordHash")] ApplicationUser applicationUser, int RoleId)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ModelState.IsValid)
            {
                using var transaction = _context.Database.BeginTransaction();

                try
                {
                    applicationUser.Id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    _context.Add(applicationUser);
                    await _context.SaveChangesAsync();

                    var newCatPro = new UserRoles()
                    {
                        //applicationRoles = RoleId,
                        //applicationUser = applicationUser.Id
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
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", applicationUser.Id);
            ViewData["UserId"] = new SelectList(_context.Companies, "Id", "Id", applicationUser.Id);
            return View(applicationUser);
        }
    }
}
