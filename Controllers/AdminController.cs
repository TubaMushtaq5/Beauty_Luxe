// AdminController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautyLuxe.Models; // Add the appropriate namespace for your models
using BeautyLuxe.Repository;

// AdminController.cs
[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly IServiceRepository _serviceRepository;
    private int _yourServiceId; // Class-level variable to store the service ID

    public AdminController(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ManageServices()
    {
        var services = _serviceRepository.GetAllServices();
        return View(services);
    }

    

}
