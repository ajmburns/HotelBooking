using HotelBooking.Data;
using HotelBooking.Models;

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
            return _context.Hotels.Where(h => h.Id == id).FirstOrDefault();
        }

        public List<Hotel> GetAll()
        {
            return _context.Hotels.ToList();
        }
    }
}
