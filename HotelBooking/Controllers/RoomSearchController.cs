using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomSearchController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<RoomSearchController> _logger;

        public RoomSearchController(IBookingService bookingService, ILogger<RoomSearchController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Room>> FindRooms(int hotelId, int numberOfPeople, DateTime startDate, DateTime endDate)
        {
            //Find available rooms on this date by querying existing bookings
            var availableRooms = _bookingService.GetAvailableRooms(hotelId, numberOfPeople, startDate, endDate);

            return new ActionResult<List<Room>>(availableRooms);
        }
    }
}
