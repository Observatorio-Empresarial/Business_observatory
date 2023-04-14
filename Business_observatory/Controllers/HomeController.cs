using Business_observatory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

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
    }
}