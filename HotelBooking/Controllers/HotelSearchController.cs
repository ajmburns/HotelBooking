using HotelBooking.Models;
using HotelBooking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelSearchController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ILogger<HotelSearchController> _logger;

        public HotelSearchController(IHotelRepository hotelRepository, ILogger<HotelSearchController> logger)
        {
            _hotelRepository = hotelRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Hotel>> FindHotels(string searchText, int page = 0)
        {
            //Minimum search characters check
            if (string.IsNullOrEmpty(searchText) || searchText.Length < 3)
            {
                return new List<Hotel>();
            }

            return _hotelRepository.SearchHotels(searchText, page);
        }
    }
}
