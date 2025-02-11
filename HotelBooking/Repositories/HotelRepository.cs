using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelBookingContext _context;

        public HotelRepository(HotelBookingContext context)
        {
            _context = context;
        }

        public Hotel GetHotel(int id)
        {
            return _context.Hotels
                .Where(h => h.Id == id)
                .Include(h => h.Rooms)
                .FirstOrDefault();
        }

        public List<Hotel> GetAll()
        {
            return _context.Hotels
                .Include(h => h.Rooms)
                .ToList();
        }

        public void AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public void DeleteAll()
        {
            foreach (var hotel in GetAll())
            {
                foreach (var room in hotel.Rooms)
                {
                    _context.Rooms.Remove(room);
                }
                _context.Hotels.Remove(hotel);
            }
            _context.SaveChanges();
        }
    }
}
