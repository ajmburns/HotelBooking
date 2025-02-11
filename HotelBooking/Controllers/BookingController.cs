using HotelBooking.Data;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<Booking> Get(string id)
        {
            return _bookingService.GetBooking(id);
        }

        [HttpPost]
        public ActionResult MakeBooking(BookingRequest request)
        {
            //Validate booking basic error checking
            if (request == null || !request.IsValid)
            {
                return BadRequest("Booking request is missing information");
            }

            try
            {
                var bookingRef = _bookingService.MakeBooking(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
