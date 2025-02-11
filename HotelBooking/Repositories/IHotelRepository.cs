using HotelBooking.Models;

namespace HotelBooking.Repositories
{
    public interface IHotelRepository
    {
        Hotel GetHotel(int id);

        List<Hotel> GetAll();
    }
}
