using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        //[Authorize(Policy = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var userAdmin=from u in _context.IdentityUsers
                          join ur in _context.IdentityUserRoles on u.Id equals ur.UserId
                          join r in _context.IdentityRoles on ur.RoleId equals r.Id
                          where r.Name=="Administrador"
                          select u;
            return View(await userAdmin.ToListAsync());
        }
        //[Authorize(Policy = "EncargadoOnly")]
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
        public async Task<IActionResult> CreateUser(IdentityUser user, string Roles)
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

                var roleName = _context.Roles
                    .Where(r => r.Id == Roles)
                    .Select(r => r.Name)
                    .FirstOrDefault();

                if (roleName != null)
                {
                    var createUser = await _userManager.CreateAsync(appUser, user.PasswordHash);
                    if (createUser.Succeeded)
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(appUser, roleName);
                        if (addToRoleResult.Succeeded)
                        {
                            // El usuario se creó y se agregó al rol exitosamente
                            await _context.SaveChangesAsync();
                            ViewData["Roles"] = new SelectList(_context.Set<IdentityRole>(), "Id", "Name");
                            ViewBag.Message = "Usuario creado con éxito";
                        }
                        else
                        {
                            ViewBag.Message = "Error al agregar el usuario al rol";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Error al crear el usuario";
                    }
                }
                else
                {
                    ViewBag.Message = "Error al crear el usuario";
                }
            }
            return View(user);
        }

        public async Task<IActionResult> RegisterUserComun()
        {
            var user= new IdentityUser();
            return View(user);
        }

        [HttpPost]
        //Registrar con rol de Usuario
        public async Task<IActionResult> RegisterUserComun(IdentityUser user)
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

                var roleName = _context.Roles
                    .Where(r => r.Name == "Usuario")
                    .Select(r => r.Name)
                    .FirstOrDefault();

                if (roleName != null)
                {
                    var createUser = await _userManager.CreateAsync(appUser, user.PasswordHash);
                    if (createUser.Succeeded)
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(appUser, roleName);
                        if (addToRoleResult.Succeeded)
                        {
                            // El usuario se creó y se agregó al rol exitosamente
                            await _context.SaveChangesAsync();
                            ViewBag.Message = "Usuario creado con éxito";
                        }
                        else
                        {
                            ViewBag.Message = "Error al agregar el usuario al rol";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Error al crear el usuario";
                    }
                }
                else
                {
                    ViewBag.Message = "Error al crear el usuario";
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
