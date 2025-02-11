using HotelBooking.Models;

namespace HotelBooking.Repositories
{
    public interface IBookingRepository
    {
        Booking GetBooking(string id);

        List<Booking> GetBookings(DateTime startDate, DateTime endDate, List<Room> rooms);

        void InsertBooking(Booking booking);
    }
}
