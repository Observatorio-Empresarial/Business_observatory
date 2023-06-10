using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business_observatory.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateUser()
        {
            var user = new IdentityUser();
            //Roles
            ViewData["Roles"] = new SelectList(_context.Set<IdentityRole>(), "Id", "Name");
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(IdentityUser user,string Roles)
        {
            if (ModelState.IsValid)
            {
                var appUser = new IdentityUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    NormalizedEmail = user.Email.ToUpper(),
                    NormalizedUserName = user.Email.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                };
                // Utilizar PasswordHasher para generar el hash de la contraseña
                var passwordHasher = new PasswordHasher<IdentityUser>();
                var hashedPassword = passwordHasher.HashPassword(appUser, user.PasswordHash);
                appUser.PasswordHash = hashedPassword;

                IdentityUserRole<string> userRole = new IdentityUserRole<string>()
                {
                    UserId = appUser.Id,
                    RoleId = Roles
                };
                // Agregar el usuario al DbSet de IdentityUsers
                var createUser = await _context.IdentityUsers.AddAsync(appUser);
                await _context.IdentityUserRoles.AddAsync(userRole);

                if (createUser != null)
                {
                    await _context.SaveChangesAsync();
                    ViewData["Roles"] = new SelectList(_context.Set<IdentityRole>(), "Id", "Name");
                    ViewBag.Message = "Usuario creado con exito";
                }
            }
            return View(user);
        }
        public async Task<IActionResult> CreateRole()
        {
            var role = new IdentityRole();
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var appRole = new IdentityRole
                {
                    Name = role.Name
                };
                var createRole = await _context.IdentityRoles.AddAsync(appRole);
                if (createRole != null)
                {
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Rol creado con exito";
                }
            }
            return View(role);
        }
    }
}
