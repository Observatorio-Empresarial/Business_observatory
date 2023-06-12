using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var userAdmin=from u in _context.IdentityUsers
                          join ur in _context.IdentityUserRoles on u.Id equals ur.UserId
                          join r in _context.IdentityRoles on ur.RoleId equals r.Id
                          where r.Name=="Administrador"
                          select u;
            return View(await userAdmin.ToListAsync());
        }
        public async Task<IActionResult> IndexEncargados()
        {
            var userAdmin = from u in _context.IdentityUsers
                            join ur in _context.IdentityUserRoles on u.Id equals ur.UserId
                            join r in _context.IdentityRoles on ur.RoleId equals r.Id
                            where r.Name == "Encargado"
                            select u;

            return View(await userAdmin.ToListAsync());
        }
        public async Task<IActionResult> IndexUsuariosComunes()
        {
            var userAdmin = from u in _context.IdentityUsers
                            join ur in _context.IdentityUserRoles on u.Id equals ur.UserId
                            join r in _context.IdentityRoles on ur.RoleId equals r.Id
                            where r.Name == ""
                            select u;
            return View(await userAdmin.ToListAsync());
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
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.Email,
                    NormalizedUserName = user.Email.ToUpper(),
                    Email = user.Email,
                    NormalizedEmail = user.Email.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = true,
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
        public async Task<IActionResult> IndexRoles()
        {
            var roles = await _context.IdentityRoles.ToListAsync();
            return View(roles);
        }
        public async Task<IActionResult> IndexUsers()
        {
            var users = await _context.IdentityUsers.ToListAsync();
            return View(users);
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
