namespace BeautyLuxe.Models
{
    public class ServiceSlot
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int SlotId { get; set; }
        public Slot Slot { get; set; }
    }
}
