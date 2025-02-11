using HotelBooking.Models;
using HotelBooking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomSearchController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<RoomSearchController> _logger;

        public RoomSearchController(IRoomRepository roomRepository, IBookingRepository bookingRepository, ILogger<RoomSearchController> logger)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Room>> FindRooms(int hotelId, int numberOfPeople, DateTime startDate, DateTime endDate)
        {
            //Find available rooms on this date by querying existing bookings
            var rooms = _roomRepository.GetRooms(hotelId, numberOfPeople);

            var existingBookings = _bookingRepository.GetBookings(startDate, endDate, rooms);

            var bookedRoomIds = existingBookings.Select(b => b.Room.Id).ToList();
            var availableRooms = rooms.Where(r => !bookedRoomIds.Contains(r.Id)).ToList();

            return new ActionResult<List<Room>>(availableRooms);
        }
    }
}
