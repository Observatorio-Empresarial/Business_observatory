using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Business_observatory.Controllers
{
    public class AssignContact : Controller
    {
        private readonly ApplicationDbContext _context;
        public AssignContact(ApplicationDbContext context)
        {
            _context = context;
        }
        //Obtener Conctactos enlazados al usuario y mostrarlos en la vista

        public async Task<IActionResult> IndexUserContacts()
        {
            var userContacts = from u in _context.IdentityUsers
                               join ur in _context.IdentityUserRoles on u.Id equals ur.UserId
                               join r in _context.IdentityRoles on ur.RoleId equals r.Id
                               join c in _context.Contactos on u.Id equals c.AspNetUserId
                               join e in _context.IdentityUsers on u.Id equals e.Id
                               where r.Name == "Encargado"
                               select new UsuarioEncargado
                               {
                                   Contacto = c,
                                   CorreoEncargado = e.Email
                               };

            var userContactsList = await userContacts.ToListAsync();

            return View(userContactsList);
        }

        public async Task<IActionResult> Index()
        {
            var userEncargado = from u in _context.IdentityUsers
                                join ur in _context.IdentityUserRoles on u.Id equals ur.UserId
                                join r in _context.IdentityRoles on ur.RoleId equals r.Id
                                where r.Name == "Encargado"
                                select u;



            ViewData["userEncargado"] = userEncargado.ToList();

            return _context.Contactos != null ?
                                                 View(await _context.Contactos.ToListAsync()) :
                                                 Problem("Entity set 'ApplicationDbContext.Contactos'  is null.");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var userEncargado = from u in _context.IdentityUsers
                                join ur in _context.IdentityUserRoles on u.Id equals ur.UserId
                                join r in _context.IdentityRoles on ur.RoleId equals r.Id
                                where r.Name == "Encargado"
                                select u;

            ViewData["userEncargado"] = userEncargado.ToList();

            if (id == null || _context.Contactos == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            //var test = new SelectList(_context.ApplicationUserRoles.Where(u => u.RoleId == "c902a4cb-06e7-44da-bcdb-ee89d2091df5"), "UserId");
            ViewData["AssignContactId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", contacto.AspNetUserId);
            //ViewData["AssignContactId"] = _context.ApplicationUserRoles.Where(u => u.RoleId == "c902a4cb-06e7-44da-bcdb-ee89d2091df5").Select(u => u.UserId).ToList();

            ViewData["EncargadosId"] = new SelectList(_context.ApplicationUserRoles.Where(u => u.RoleId == "c902a4cb-06e7-44da-bcdb-ee89d2091df5"), "UserId");

            return View(contacto);
        }

        // POST: Contactoes with AspeUsersId/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditU(int id, [Bind("Id,Nombre,Apellido,Email,Telefono,Mensaje,Estado,FechaCreacion,AspNetUserId")] Contacto contacto)
        {
            if (id != contacto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Cambiar estado
                    contacto.Estado = "Asignado";
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.Id))
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
            return View(contacto);
        }

        private bool ContactoExists(int id)
        {
            return (_context.Contactos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
