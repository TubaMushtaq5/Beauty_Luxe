﻿using BeautyLuxe.Areas.Identity.Data;

namespace BeautyLuxe.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int SlotId { get; set; }
        public Slot Slot { get; set; }
        
    }
}
