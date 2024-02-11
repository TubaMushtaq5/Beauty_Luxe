using System.Collections.Generic;
using System.Linq;
using BeautyLuxe.Areas.Identity.Data;
using BeautyLuxe.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyLuxe.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public Service AddService(Service service)
        {
            _context.Services.Add(service);

            foreach (var slot in service.Slots)
            {
                slot.IsAvailable = true;
                _context.Slot.Add(slot);
            }

            _context.SaveChanges();
            return service;
        }

        public void RemoveExpiredSlots()
        {
            var expiredSlots = _context.Slot
                .Include(s => s.Booking) 
                .Where(s => s.SlotTime < DateTime.Now)
                .ToList();


            foreach (var slot in expiredSlots)
            {
                if (!slot.IsAvailable)
                {
                    if (slot.Booking != null)
                    {
                        _context.Bookings.Remove(slot.Booking);
                    }

                }
                _context.Slot.Remove(slot);
            }

            _context.SaveChanges();
        }

        public Service UpdateService(Service modifiedService)
        {
            
            var existingService = _context.Services
                .Include(s => s.Slots)
                .FirstOrDefault(s => s.Id == modifiedService.Id);

            if (existingService != null)
            {
                existingService.Name = modifiedService.Name;
                existingService.Description = modifiedService.Description;
                existingService.Price = modifiedService.Price;

               
                foreach (var modifiedSlot in modifiedService.Slots)
                {
                    if (modifiedSlot.Id == 0)
                    {
                        modifiedSlot.IsAvailable = true;
                        existingService.Slots.Add(modifiedSlot);
                    }

                    else
                    {
                        var existingSlot = existingService.Slots.FirstOrDefault(s => s.Id == modifiedSlot.Id);

                        if (existingSlot != null)
                        {

                            // Update existing slot properties
                            existingSlot.SlotTime = modifiedSlot.SlotTime;
                        }
                        else
                        {
                            // If the slot doesn't exist, add it
                            modifiedSlot.IsAvailable = true;
                            existingService.Slots.Add(modifiedSlot);
                        }
                    }
                   
                    
                }
                _context.SaveChanges();
            }

            return existingService;
        }



        public Service DeleteService(Service service)
        {
            var serviceToDelete = _context.Services
                .Include(s => s.Slots)
                .FirstOrDefault(s => s.Id == service.Id);

            if (serviceToDelete != null)
            {
                _context.Slot.RemoveRange(serviceToDelete.Slots);

                _context.Services.Remove(serviceToDelete);

                _context.SaveChanges();
            }

            return serviceToDelete;
        }

        public IEnumerable<Service> GetAllServices()
        {
            
            return _context.Services.ToList();
        }

        public Service GetServiceById(int id)
        {
            var service = _context.Services
                .Include(s => s.Slots) 
                .FirstOrDefault(s => s.Id == id);

            return service;

        }

        public Slot GetSlotById(int id)
        {
            return _context.Slot.Find(id);
        }

        public IEnumerable<Slot> GetSlotsForService(int serviceId)
        {
            var slotsForService = _context.Slot.Where(slot => slot.ServiceId == serviceId).ToList();

            return slotsForService;
        }

        public void DeleteSlot(Slot slot)
        {
            if (!slot.IsAvailable)
            {
                if (slot.Booking != null)
                {
                    _context.Bookings.Remove(slot.Booking);
                }

            }
            _context.Slot.Remove(slot);
            _context.SaveChanges ();
        }
    }
}
