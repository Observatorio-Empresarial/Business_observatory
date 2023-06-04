using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
                View(await _context.ApplicationUser.ToListAsync()) : Problem("Error");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ApplicationUser();
            ViewData["RoleId"] = new SelectList(_context.ApplicationRole, "Id", "Name");

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email,PhoneNumber,PasswordHash")] ApplicationUser applicationUser, string roleId = "d2151067-ea20-4978-a78b-9a95eaab311a")
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ModelState.IsValid)
            {
                using var transaction = _context.Database.BeginTransaction();

                try
                {
                    applicationUser.EmailConfirmed = true;

                    // Encriptar la contraseña
                    var passwordHasher = new PasswordHasher<ApplicationUser>();
                    var hashedPassword = passwordHasher.HashPassword(applicationUser, applicationUser.PasswordHash);
                    applicationUser.PasswordHash = hashedPassword;
                    _context.Add(applicationUser);
                    await _context.SaveChangesAsync();

                    var userRole = new IdentityUserRole<string>()
                    {
                        UserId = applicationUser.Id,
                        RoleId = roleId
                    };
                    _context.Add(userRole);
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

            ViewData["RoleId"] = new SelectList(_context.ApplicationRole, "Id", "Id");
            return View(applicationUser);
        }
    }
}
