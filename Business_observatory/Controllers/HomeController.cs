using Business_observatory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Business_observatory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Business_observatory.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Repository()
        {
            return View();
        }

        public IActionResult HelpRequest()
        {
            return View();
        }

        public IActionResult HelpManage()
        {
            return View();
        }

        public IActionResult Contact()
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

        //public void sendMail()
        //{
        //    //Send Email-----------------------------------------------------------------------------
        //    MailMessage correo = new MailMessage();
        //    correo.From = new MailAddress("devinamurrioUnivalle@gmail.com", "Kyocode", System.Text.Encoding.UTF8);//Correo de salida
        //    correo.To.Add("ald0029696@est.univalle.edu"); //Correo destino?
        //    correo.Subject = "Datos de Login"; //Asunto
        //    correo.Body = "Password: "; //Mensaje del correo
        //    correo.IsBodyHtml = true;
        //    correo.Priority = MailPriority.Normal;
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
        //    smtp.Port = 587; //Puerto de salida
        //    smtp.Credentials = new NetworkCredential("devinamurrioUnivalle@gmail.com", "zpksehcoeyqqqsol");//Cuenta de correo
        //    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        //    smtp.EnableSsl = true;//True si el servidor de correo permite ssl
        //    smtp.Send(correo);
        //}

        //Thread backgroundThread = new Thread(new ThreadStart(sendMail));
        //// Start thread  
        //backgroundThread.Start();

        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\IMG\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                TempData["data"] = fileName + "" + extension;

            }
            TempData["Message"] = "File successfully uploaded to File System.";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UploadToFileSystem2(List<IFormFile> files1, List<IFormFile> files2, List<IFormFile> files3, List<IFormFile> files4)
        {
            foreach (var file1 in files1)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\IMG1\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file1.FileName);
                var filePath = Path.Combine(basePath, file1.FileName);
                var extension = Path.GetExtension(file1.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file1.CopyToAsync(stream);
                    }
                }
                TempData["data1"] = fileName + "" + extension;
            }

            foreach (var file2 in files2)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\IMG2\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file2.FileName);
                var filePath = Path.Combine(basePath, file2.FileName);
                var extension = Path.GetExtension(file2.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file2.CopyToAsync(stream);
                    }
                }
                TempData["data2"] = fileName + "" + extension;
            }

            foreach (var file3 in files3)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\IMG3\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file3.FileName);
                var filePath = Path.Combine(basePath, file3.FileName);
                var extension = Path.GetExtension(file3.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file3.CopyToAsync(stream);
                    }
                }
                TempData["data3"] = fileName + "" + extension;
            }


            foreach (var file4 in files4)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\IMG4\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file4.FileName);
                var filePath = Path.Combine(basePath, file4.FileName);
                var extension = Path.GetExtension(file4.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file4.CopyToAsync(stream);
                    }
                }
                TempData["data4"] = fileName + "" + extension;
            }

            TempData["Message"] = "File successfully uploaded to File System.";

            return RedirectToAction("Index");
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects.Include(p => p.ApplicationUser).Include(p => p.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ApplicationUser)
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }


    }
}