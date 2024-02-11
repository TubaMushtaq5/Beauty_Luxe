namespace BeautyLuxe.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public DateTime SlotTime { get; set; }
        public bool IsAvailable { get; set; }
        public Service Service { get; set; }
        public int ServiceId { get; set; }
        public Booking Booking { get; set; }
        public int BookingId { get; set;}
    }
}
