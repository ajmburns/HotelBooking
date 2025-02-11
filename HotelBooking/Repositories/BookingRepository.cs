using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelBookingContext _context;

        public BookingRepository(HotelBookingContext context)
        {
            _context = context;
        }

        public Booking GetBooking(string id)
        {
            return _context.Bookings
                .Where(b => b.Id == id)
                .Include(b => b.Hotel)
                .Include(b => b.Room)
                .FirstOrDefault();
        }

        public List<Booking> GetBookings(DateTime startDate, DateTime endDate, List<Room> rooms)
        {
            var roomIds = rooms.Select(r => r.Id).ToList();

            var bookingsForRoom = _context.Bookings.Where(b => roomIds.Contains(b.Room.Id)).ToList();
            var bookingsForStartDate = _context.Bookings.Where(b => (startDate.Date >= b.StartDate && startDate.Date <= b.EndDate)).ToList();
            var bookingsForEndDate = _context.Bookings.Where(b => (endDate.Date >= b.StartDate && endDate.Date <= b.EndDate)).ToList();

            var bookings = _context.Bookings
                .Where(b =>
                    ((startDate.Date >= b.StartDate && startDate.Date <= b.EndDate) || (endDate.Date >= b.StartDate && endDate.Date <= b.EndDate))
                    && roomIds.Contains(b.Room.Id))
                .ToList();

            return bookings;
        }

        public void InsertBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
    }
}
