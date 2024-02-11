// UserController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautyLuxe.Models;
using BeautyLuxe.Repository;
using System.Security.Claims;


[Authorize(Roles = "client")]
public class UserController : Controller
{
    private readonly IServiceRepository _serviceRepository;


    public UserController(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
       
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ViewServices()
    {
        var services = _serviceRepository.GetAllServices();
        return View(services);
    }

}
