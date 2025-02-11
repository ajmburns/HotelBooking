using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingContext _context;

        public RoomRepository(HotelBookingContext context)
        {
            _context = context;
        }

        public Room GetRoom(int roomId)
        {
            return _context.Rooms.Where(r => r.Id == roomId).FirstOrDefault();
        }

        public List<Room> GetRooms(int hotelId, int numberOfPeople)
        {
            var hotel = _context.Hotels.Include(h => h.Rooms).Where(h => h.Id == hotelId).FirstOrDefault();

            return hotel == null ? [] : hotel.Rooms.Where(r => r.Capacity >= numberOfPeople).ToList();
        }
    }
}
