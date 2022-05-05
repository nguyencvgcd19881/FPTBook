using FPTBook.Areas.Identity.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FPTBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<FPTBookUser> _userManager;
        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, UserManager<FPTBookUser> userManager)
        {
            _logger = logger;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        [Authorize(Roles = "Customer")]
        public IActionResult ForCustomerOnly()
        {
            ViewBag.message = "This is for Customer only! Hi " + _userManager.GetUserName(HttpContext.User);
            return View("Views/Home/Index.cshtml");
        }

        [Authorize(Roles = "Seller")]
        public IActionResult ForSellerOnly()
        {
            ViewBag.message = "This is for Store Owner only!";
            return View("Views/Home/Index.cshtml");
        }

        public IActionResult Index()
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