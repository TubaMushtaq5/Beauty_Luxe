using System.Collections.Generic;
using BeautyLuxe.Models;

namespace BeautyLuxe.Repository
{
    public interface IServiceRepository
    {
        Service AddService(Service service);
        Service UpdateService(Service service);
        Service DeleteService(Service service);
        IEnumerable<Service> GetAllServices();
        Service GetServiceById(int id);
        Slot GetSlotById(int id);   
        
        IEnumerable<Slot> GetSlotsForService(int serviceId);

        void DeleteSlot(Slot slot);
        void RemoveExpiredSlots();

    }
}
