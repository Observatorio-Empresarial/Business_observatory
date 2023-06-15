using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;
using System.Text.RegularExpressions;

namespace Business_observatory.Controllers
{
    public class ArchivoesController : Controller
    {
        private static string ApiKey = "AIzaSyA4DjsG7hKblkmS8RO9JluoKCS4Gk_y_f8";
        private static string Bucket = "taskmanager-62211.appspot.com";
        private static string AuthEmail = "apalamarcelo@gmail.com";
        private static string AuthPassword = "Admin_123";

        private readonly ApplicationDbContext _context;

        public ArchivoesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> DownloadFile(int id)
        {
            // Download file from Firebase Storage
            var file = await _context.Archivos.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null)
                return null;

            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var authResult = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var storage = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authResult.FirebaseToken),
                    ThrowOnCancel = true
                });

            var filePath = "archivos/" + file.Nombre ;
            var downloadUrl = await storage.Child(filePath).GetDownloadUrlAsync();

            using (var webClient = new WebClient())
            {
                var fileData = webClient.DownloadData(downloadUrl);

                var provider = new FileExtensionContentTypeProvider();
                string contentType;
                if (!provider.TryGetContentType(file.Nombre, out contentType))
                {
                    contentType = "application/octet-stream";
                }

                // Eliminar caracteres inválidos del nombre del archivo
                var sanitizedFileName = SanitizeFileName(file.Nombre);

                return File(fileData, contentType, sanitizedFileName);
            }
        }

        private string SanitizeFileName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitizedFileName = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
            return Regex.Replace(sanitizedFileName, @"[\s-]+", "_");
        }

        // GET: Archivoes
        public async Task<IActionResult> Index(int id)
        {
            TempData["Id"] = id;
            var applicationDbContext = _context.Archivos.Include(a => a.Proyectos);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> PreviewList()
        {
            var applicationDbContext = _context.Archivos.Include(a => a.Proyectos);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            var applicationDbContext = _context.Archivos.Include(a => a.Proyectos);
            return View(await applicationDbContext.ToListAsync());
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

        private static async Task<string> SubirArchivo(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Autenticación con Firebase
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));

                var authResult = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // Crear instancia de FirebaseStorage
                var storage = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(authResult.FirebaseToken),
                        ThrowOnCancel = true // Cuando se cancela la carga, se lanza una excepción. Por defecto, no se lanza ninguna excepción.
                    });

                // Ruta en la que deseas almacenar la imagen en Firebase Storage
                string storagePath = "archivos/" + file.FileName;

                // Subir la imagen a Firebase Storage
                using (var stream = file.OpenReadStream())
                {
                    var task = storage.Child(storagePath).PutAsync(stream);
                    try
                    {
                        // Obtener la URL de descarga de la imagen
                        var downloadUrl = await task;

                        // Retornar la URL de descarga de la imagen
                        return downloadUrl;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

            return null; // Si no se pudo subir la imagen, retorna null
        }
        // GET: Archivoes/Create
        public IActionResult Create()
        {
            ViewData["ProyectosId"] = new SelectList(_context.Proyectos, "Id", "Nombre");
            return View();
        }

        // POST: Archivoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Tipo,Extension,FechaSubida,ProyectosId")] Archivo archivo, int ProyectosId, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // Subir el archivo a Firebase Storage y obtener la URL de descarga
                string downloadUrl = await SubirArchivo(file);

                // Asignar la URL de descarga al objeto Archivo
                archivo.Nombre = file.FileName;
                //obtener la extension del archivo ej: .pdf
                archivo.Tipo = file.ContentType;
                archivo.Extension = downloadUrl;
                archivo.FechaSubida = DateTime.Now;
                archivo.ProyectosId = ProyectosId;

                _context.Add(archivo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Proyectoes");
            }
            ViewData["ProyectosId"] = new SelectList(_context.Proyectos, "Id", "Nombre");
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
    }
}
