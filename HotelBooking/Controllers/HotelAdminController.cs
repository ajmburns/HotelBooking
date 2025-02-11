using HotelBooking.Data;
using HotelBooking.Models;
using HotelBooking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    /// <summary>
    /// Admin controller for Hotel Data create/reset
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HotelAdminController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ILogger<HotelAdminController> _logger;

        public HotelAdminController(IHotelRepository hotelRepository, ILogger<HotelAdminController> logger)
        {
            _hotelRepository = hotelRepository;
            _logger = logger;
        }

        /// <summary>
        /// Seed the database with a list of hotels, initialise each hotel with 6 rooms.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>Performance gotcha - may not scale to handle a large list of hotels.</remarks>
        [HttpPost]
        public ActionResult Seed(HotelDataRequest request)
        {
            //Exit if no data
            if (request?.Hotels == null || request.Hotels.Count <= 0)
            {
                return Ok();
            }

            try
            {
                //Note: uses basic "AddHotel" method, adds hotels one-by-one, will not scale to large datasets
                foreach (var hotel in request.Hotels)
                {
                    //Simplification - assumes each hotel has exactly 6 rooms, two of each type

                    var rooms = new List<Room>()
                    {
                        BuildRoom(RoomType.Single),
                        BuildRoom(RoomType.Single),
                        BuildRoom(RoomType.Double),
                        BuildRoom(RoomType.Double),
                        BuildRoom(RoomType.Deluxe),
                        BuildRoom(RoomType.Deluxe),
                    };

                    var newHotel = new Hotel { Name = hotel.Name, Rooms = rooms };

                    _hotelRepository.AddHotel(newHotel);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Reset()
        {
            _hotelRepository.DeleteAll();
            return Ok();
        }

        private Room BuildRoom(RoomType roomType)
        {
            int capacity;
            switch (roomType)
            {
                case RoomType.Single:
                    capacity = 1;
                    break;
                case RoomType.Double:
                case RoomType.Deluxe:
                    capacity = 2;
                    break;
                default:
                    capacity = 0;
                    break;
            }

            return new Room { RoomType = roomType, Capacity = capacity, Description = $"{roomType} room, max {capacity} occupant(s)" };
        }
    }
}
