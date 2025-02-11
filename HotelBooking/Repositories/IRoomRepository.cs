using HotelBooking.Models;

namespace HotelBooking.Repositories
{
    public interface IRoomRepository
    {
        Room GetRoom(int roomId);

        List<Room> GetRooms(int hotelId, int numberOfPeople);
    }
}
