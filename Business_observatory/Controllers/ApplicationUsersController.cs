using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        [Authorize(Roles ="Administrador")]
        public async Task<IActionResult> Index()
        {
            return _context.ApplicationUsers != null ?
                        View(await _context.ApplicationUsers.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.ApplicationRole'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ApplicationUser();
            ViewData["RoleId"] = new SelectList(_context.ApplicationRoles, "Id", "Name");

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email,PhoneNumber,PasswordHash")] ApplicationUser applicationUser, string roleId)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!ModelState.IsValid)
            {
                using var transaction = _context.Database.BeginTransaction();
                try
                {
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
            ViewData["RoleId"] = new SelectList(_context.ApplicationRoles, "Id", "Id");
            return View(applicationUser);
        }

    }
}
