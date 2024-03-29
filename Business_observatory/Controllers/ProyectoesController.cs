﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Identity;

namespace Business_observatory.Controllers
{
    public class ProyectoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProyectoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proyectoes
        public async Task<IActionResult> Index()
        {
            //Obteber rol del usuario logueado

            var userRole = from r in _context.Roles
                           join ur in _context.UserRoles on r.Id equals ur.RoleId
                           join u in _context.Users on ur.UserId equals u.Id
                           where u.UserName == User.Identity.Name
                           select r.Name;

            //user con rol encargado
            //var userEncargado = from u in _context.IdentityUsers
            //                    join ur in _context.IdentityUserRoles on u.Id equals ur.UserId
            //                    join r in _context.IdentityRoles on ur.RoleId equals r.Id
            //                    where r.Name == "Encargado"
            //                    select u;

            //ViewData["userEncargado"] = userEncargado.ToList();

            ViewData["userRole"] = userRole.FirstOrDefault();


            var applicationDbContext = _context.Proyectos.Include(p => p.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Proyectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proyectos == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyectoes/Create
        public IActionResult Create()
        {
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            ViewData["CategoriasId"] = new SelectList(_context.Set<Categoria>(), "Id", "Nombre");
            return View();
        }

        // POST: Proyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,FechaInicio,FechaFinalizacion,Responsable,Empresa,FechaCreacion,AspNetUserId,UserId")] Proyecto proyecto,int categoriaId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyecto);
                await _context.SaveChangesAsync();

                string query= $"INSERT INTO categoriaproyecto (ProyectosId, CategoriasId) VALUES ({proyecto.Id}, {categoriaId})";
                await _context.Database.ExecuteSqlRawAsync(query);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", proyecto.AspNetUserId);
            ViewData["CategoriasId"] = new SelectList(_context.Set<Categoria>(), "Id", "Nombre");

            return View(proyecto);
        }

        // GET: Proyectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proyectos == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", proyecto.AspNetUserId);
            return View(proyecto);
        }

        // POST: Proyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,FechaInicio,FechaFinalizacion,Responsable,Empresa,FechaCreacion,AspNetUserId")] Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.Id))
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
            ViewData["AspNetUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", proyecto.AspNetUserId);
            return View(proyecto);
        }

        // GET: Proyectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proyectos == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proyectos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Proyectos'  is null.");
            }
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
          return (_context.Proyectos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
