using BeautyLuxe.Models;

namespace BeautyLuxe.Repository
{
    public interface IBookingRepository
    {
        void BookAppointment(Booking booking);
        IEnumerable<Booking> GetBookingsByUserId(string userId);
        Booking GetBookingById(int bookingId);
        void UpdateAppointment(Booking updatedBooking);
        void CancelAppointment(int bookingId);
        IEnumerable<Booking> GetAllBookings();
    }
}
