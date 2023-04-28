using Business_observatory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using Business_observatory.Data;

namespace Business_observatory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

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

        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\VID\\");
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
                TempData["data4"] = fileName + "" + extension;

            }
            TempData["Message"] = "File successfully uploaded to File System.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> UploadToFileSystem2(List<IFormFile> files1, List<IFormFile> files2, List<IFormFile> files3)
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
            TempData["Message"] = "File successfully uploaded to File System.";

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}