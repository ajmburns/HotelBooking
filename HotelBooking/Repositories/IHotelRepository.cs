using HotelBooking.Models;

namespace HotelBooking.Repositories
{
    public interface IHotelRepository
    {
        Hotel GetHotel(int id);

        List<Hotel> GetList(int page = 0);

        void AddHotel(Hotel hotel);

        void DeleteAll();

        List<Hotel> SearchHotels(string searchText, int page = 0);
    }
}
