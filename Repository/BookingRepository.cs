using BeautyLuxe.Areas.Identity.Data;
using BeautyLuxe.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyLuxe.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public void BookAppointment(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public Booking GetBookingById(int bookingId)
        {
            return _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Service)
                .Include(b => b.Slot)
                .FirstOrDefault(b => b.Id == bookingId);
        }

        public IEnumerable<Booking> GetBookingsByUserId(string userId)
        {
            return _context.Bookings
                .Include(b => b.Service)
                .Include(b => b.Slot)
                .Where(b => b.UserId == userId)
                .ToList();
        }

        public void UpdateAppointment(Booking updatedBooking)
        {

            _context.SaveChanges();
        }

        public void CancelAppointment(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Bookings
                .Include(b => b.Service)
                .Include(b => b.Slot)
                .Include(b=> b.User)
                .ToList();
        }
    }
}
