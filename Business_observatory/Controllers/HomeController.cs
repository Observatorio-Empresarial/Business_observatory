using Business_observatory.Data;
using Business_observatory.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace Business_observatory.Controllers
{
    public class HomeController : Controller
	{
        private static string ApiKey = "AIzaSyA4DjsG7hKblkmS8RO9JluoKCS4Gk_y_f8";
        private static string Bucket = "taskmanager-62211.appspot.com";
        private static string AuthEmail = "apalamarcelo@gmail.com";
        private static string AuthPassword = "Admin_123";

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Proyectos()
        {
            var applicationDbContext = _context.Proyectos.Include(p => p.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Archivos(int id)
        {
            TempData["Id"] = id;
            var applicationDbContext = _context.Archivos.Include(a => a.Proyectos);
            return View(await applicationDbContext.ToListAsync());
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

            var filePath = "archivos/" + file.Nombre;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Telefono,Message,Estado,FechaCreacion")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                contacto.Estado = "Pendiente";
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(contacto);
        }

        public async Task<IActionResult> Index()
		{
            return View();
		}
		public IActionResult Nosotros()
		{
			return View();
		}
		public IActionResult Administracion()
		{
			return View();
		}
		public IActionResult Contactenos()
		{
			return View();
		}
		public IActionResult Cuenta()
		{
			return View();
		}
		public IActionResult CrearCuenta()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}