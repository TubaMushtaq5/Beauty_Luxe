using BeautyLuxe.Areas.Identity.Data;
using BeautyLuxe.Models;
using BeautyLuxe.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BeautyLuxe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IServiceRepository _serviceRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, IServiceRepository serviceRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Temp()
        {
            
            _serviceRepository.RemoveExpiredSlots();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, "admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "client"))
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var services = _serviceRepository.GetAllServices();
            return View(services);
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