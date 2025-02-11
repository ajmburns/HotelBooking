using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private const int PageSize = 5;

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

        public List<Hotel> GetList(int page = 0)
        {
            return _context.Hotels
                .Include(h => h.Rooms)
                .Skip(page * PageSize)
                .Take(PageSize)
                .ToList();
        }

        private List<Hotel> GetAll()
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
            foreach (var booking in _context.Bookings)
            {
                _context.Bookings.Remove(booking);
            }

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

        public List<Hotel> SearchHotels(string searchText, int page = 0)
        {
            return _context.Hotels
                .Where(h => EF.Functions.Like(h.Name, $"%{searchText}%"))
                .Include(h => h.Rooms)
                .Skip(page * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
