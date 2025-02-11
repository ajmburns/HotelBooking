using HotelBooking.Models;
using HotelBooking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {

        private readonly IHotelRepository _hotelRepository;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IHotelRepository hotelRepository, ILogger<HotelController> logger)
        {
            _hotelRepository = hotelRepository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            return _hotelRepository.GetHotel(id);
        }

        [HttpGet]
        public ActionResult<List<Hotel>> GetList(int page = 0)
        {
            return _hotelRepository.GetList(page);
        }
    }
}
