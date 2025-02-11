using HotelBooking.Data;
using HotelBooking.Helpers;
using HotelBooking.Models;
using HotelBooking.Repositories;

namespace HotelBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IHotelRepository hotelRepository, IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public Booking GetBooking(string id)
        {
            return _bookingRepository.GetBooking(id);
        }

        public List<Room> GetAvailableRooms(int hotelId, int numberOfPeople, DateTime startDate, DateTime endDate)
        {
            //Find available rooms on this date by querying existing bookings
            var rooms = _roomRepository.GetRooms(hotelId, numberOfPeople);
            var existingBookings = _bookingRepository.GetBookings(startDate, endDate, rooms);
            var bookedRoomIds = existingBookings.Select(b => b.Room.Id).ToList();
            return rooms.Where(r => !bookedRoomIds.Contains(r.Id)).ToList();
        }

        public string MakeBooking(BookingRequest request)
        {
            //Don't assume the rooms are still available, even if payment has been taken.
            //For production use would probably require making provisional reservation, wait for payment confirmation, then confirm booking.

            var availableRooms = GetAvailableRooms(request.HotelId, request.NumberOfPeople, request.StartDate, request.EndDate);

            if (!availableRooms.Any(r => r.Id == request.RoomId))
            {
                throw new Exception("Room is already booked on the selected dates");
            }

            //TODO: could possibly check booking availability by generating a booking reference for the request
            //and checking against existing bookings without fetching hotel rooms (faster query, potentially less reliable).

            var hotel = _hotelRepository.GetHotel(request.HotelId);
            var room = hotel?.Rooms.FirstOrDefault(r => r.Id == request.RoomId);

            if (hotel == null || room == null || room.Capacity < request.NumberOfPeople)
            {
                throw new Exception("Booking request is missing information");
            }

            //Note: Use simple in-memory booking reference generator, not DB-generated sequence
            var booking = new Booking
            {
                Hotel = hotel,
                Room = room,
                Name = request.CustomerName,
                PaymentReference = request.PaymentReference,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                NumberOfPeople = request.NumberOfPeople,
                Id = BookingReferenceHelper.GenerateBookingReference(request)
            };

            _bookingRepository.InsertBooking(booking);

            return booking.Id;
        }
    }
}
