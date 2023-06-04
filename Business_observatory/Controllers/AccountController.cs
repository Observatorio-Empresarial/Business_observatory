using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Business_observatory.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterLogin()
        {
            var model = new ApplicationUser();
            ViewData["RoleId"] = new SelectList(_context.ApplicationRole, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterLogin(ApplicationUser model, string roleId)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Apellido = model.Apellido, Compania = model.Compania, Email = model.Email, Ciudad = model.Ciudad };

                var result = await _userManager.CreateAsync(user, model.PasswordHash);
                if (result.Succeeded)
                {
                    // Asignar el rol seleccionado al nuevo usuario
                    var role = await _context.ApplicationRole.FindAsync(roleId);
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }

                    // Resto del código...

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewData["RoleId"] = new SelectList(_context.ApplicationRole, "Id", "Name", roleId);
            return View(model);
        }
    }
}
