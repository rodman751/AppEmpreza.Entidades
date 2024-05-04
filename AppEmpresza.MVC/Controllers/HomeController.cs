using AppEmpresza.MVC.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppEmpresza.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public INotyfService _notifyService { get; }
       

        public HomeController(ILogger<HomeController> logger, INotyfService notyfService)
        {
            _logger = logger;
            _notifyService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            _notifyService.Success("Bienvenido a la página de privacidad");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
