using HotelBooking.Data;
using HotelBooking.Helpers;
using HotelBooking.Models;
using HotelBooking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IHotelRepository hotelRepository, IRoomRepository roomRepository, IBookingRepository bookingRepository, ILogger<BookingController> logger)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<Booking> Get(string id)
        {
            return _bookingRepository.GetBooking(id);
        }

        [HttpPost]
        public ActionResult MakeBooking(BookingRequest request)
        {
            //Validate booking basic error checking
            if (request == null || !request.IsValid)
            {
                return BadRequest("Booking request is missing information");
            }

            //Don't assume the rooms are still available, even if payment has been taken.
            //For production use would probably require making provisional reservation, wait for payment confirmation, then lock-in booking.

            var hotel = _hotelRepository.GetHotel(request.HotelId);
            var room = _roomRepository.GetRoom(request.RoomId);

            if (hotel != null && room != null && room.Capacity >= request.NumberOfPeople)
            {
                //Note: Use simple in-memory booking reference generator, not DB-generated sequence
                var booking = new Booking
                {
                    Hotel = hotel,
                    Room = room,
                    Name = request.CustomerName,
                    PaymentReference = request.PaymentReference,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Id = BookingReferenceHelper.GenerateBookingReference(request)
                };

                _bookingRepository.InsertBooking(booking);

                return Ok(booking.Id);
            }

            return BadRequest("Booking request is missing information");
        }
    }
}
