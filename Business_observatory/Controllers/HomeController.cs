using Business_observatory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Business_observatory.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Nosotros()
		{
			return View();
		}
		public IActionResult Proyectos()
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