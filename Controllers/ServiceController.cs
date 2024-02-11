using BeautyLuxe.Models;
using BeautyLuxe.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace BeautyLuxe.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }


        public IActionResult Index()
        {
            var services = _serviceRepository.GetAllServices();
            return View(services);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _serviceRepository.RemoveExpiredSlots();

            var service = _serviceRepository.GetServiceById(id.Value);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create(Service service)
        {
           
            
            _serviceRepository.AddService(service);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = _serviceRepository.GetServiceById(id.Value);
            

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Edit(Service modifiedService)
        {
            Console.WriteLine(modifiedService.Slots.Count);
            _serviceRepository.UpdateService(modifiedService);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            Service service = _serviceRepository.GetServiceById(id.Value);
            _serviceRepository.DeleteService(service);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeleteSlot(int slotId)
        {
            var slot = _serviceRepository.GetSlotById(slotId);

            if (slot != null)
            {
                _serviceRepository.DeleteSlot(slot);
                return Ok(); 
            }

            return NotFound();
        }

    }
}
