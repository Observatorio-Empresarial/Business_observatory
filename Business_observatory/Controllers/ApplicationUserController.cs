using Microsoft.AspNetCore.Mvc;

namespace Business_observatory.Controllers
{
    public class ApplicationUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
