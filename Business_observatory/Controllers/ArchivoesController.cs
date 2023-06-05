using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Business_observatory.Data;
using Business_observatory.Models;

namespace Business_observatory.Controllers
{
    public class ArchivoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArchivoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Archivoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Archivos.Include(a => a.Proyectos);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> DownloadFileFromDatabase(int id)
        {

            var file = await _context.FilesOnDatabase.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null) return null;
            return File(file.Data, file.Tipo, file.Nombre + file.Extension);
        }

        public async Task<IActionResult> DeleteFileFromDatabase(int id)
        {

            var file = await _context.FilesOnDatabase.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.FilesOnDatabase.Remove(file);
            _context.SaveChanges();
            TempData["Message"] = $"Removed {file.Nombre + file.Extension} successfully from Database.";
            return RedirectToAction("Index");
        }

        // GET: Archivoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Archivos == null)
            {
                return NotFound();
            }

            var archivo = await _context.Archivos
                .Include(a => a.Proyectos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archivo == null)
            {
                return NotFound();
            }

            return View(archivo);
        }

        // GET: Archivoes/Create
        public IActionResult Create()
        {
            ViewData["ProyectosId"] = new SelectList(_context.Proyectos, "Id", "Id");
            return View();
        }

        // POST: Archivoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Tipo,Extension,FechaSubida,ProyectosId")] Archivo archivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(archivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProyectosId"] = new SelectList(_context.Proyectos, "Id", "Id", archivo.ProyectosId);
            return View(archivo);
        }

        // GET: Archivoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Archivos == null)
            {
                return NotFound();
            }

            var archivo = await _context.Archivos.FindAsync(id);
            if (archivo == null)
            {
                return NotFound();
            }
            ViewData["ProyectosId"] = new SelectList(_context.Proyectos, "Id", "Id", archivo.ProyectosId);
            return View(archivo);
        }

        // POST: Archivoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Tipo,Extension,FechaSubida,ProyectosId")] Archivo archivo)
        {
            if (id != archivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(archivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArchivoExists(archivo.Id))
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
            ViewData["ProyectosId"] = new SelectList(_context.Proyectos, "Id", "Id", archivo.ProyectosId);
            return View(archivo);
        }

        // GET: Archivoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Archivos == null)
            {
                return NotFound();
            }

            var archivo = await _context.Archivos
                .Include(a => a.Proyectos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archivo == null)
            {
                return NotFound();
            }

            return View(archivo);
        }

        // POST: Archivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Archivos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Archivos'  is null.");
            }
            var archivo = await _context.Archivos.FindAsync(id);
            if (archivo != null)
            {
                _context.Archivos.Remove(archivo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArchivoExists(int id)
        {
          return (_context.Archivos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> UploadToDatabase(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                string proyectoId = Request.Form["txtNombre"];
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var fileModel = new FileOnDatabaseModel
                {
                    FechaSubida = DateTime.UtcNow,
                    Tipo = file.ContentType,
                    Extension = extension,
                    Nombre = fileName,
                    Descripcion = description,
                    ProyectosId = int.Parse(proyectoId),
                };
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }
                _context.FilesOnDatabase.Add(fileModel);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
