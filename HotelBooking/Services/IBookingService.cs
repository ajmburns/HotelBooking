using HotelBooking.Data;
using HotelBooking.Models;

namespace HotelBooking.Services
{
    public interface IBookingService
    {
        Booking GetBooking(string id);

        List<Room> GetAvailableRooms(int hotelId, int numberOfPeople, DateTime startDate, DateTime endDate);

        string MakeBooking(BookingRequest request);
    }
}
