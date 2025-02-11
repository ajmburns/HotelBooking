using FluentAssertions;
using HotelBooking.Data;
using HotelBooking.Models;
using HotelBooking.Repositories;
using HotelBooking.Services;
using HotelBookingApi.Test.Builders;
using Moq;

namespace HotelBookingApi.Test.Services
{
    [TestClass]
    public class BookingServiceTests
    {
        private IBookingService? _service;

        private Mock<IHotelRepository>? _hotelRepository;
        private Mock<IRoomRepository>? _roomRepository;
        private Mock<IBookingRepository>? _bookingRepository;

        [TestInitialize]
        public void Setup()
        {
            _hotelRepository = new Mock<IHotelRepository>();
            _roomRepository = new Mock<IRoomRepository>();
            _bookingRepository = new Mock<IBookingRepository>();

            _service = new BookingService(_hotelRepository.Object, _roomRepository.Object, _bookingRepository.Object);
        }

        [TestMethod]
        public void GetAvailableRooms_WhereNoBookingsOnSpecifiedDate_ReturnAllRooms()
        {
            var rooms = new List<Room>() { new RoomBuilder().WithId(21).WithCapacity(1).Build(), new RoomBuilder().WithId(22).WithCapacity(2).Build(), };
            var hotel = new Hotel { Id = 1, Name = "Desert Inn", Rooms = rooms };

            _hotelRepository.Setup(r => r.GetHotel(It.IsAny<int>())).Returns(hotel);
            _roomRepository.Setup(r => r.GetRooms(It.IsAny<int>(), It.IsAny<int>())).Returns(rooms);
            _bookingRepository.Setup(r => r.GetBookings(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<List<Room>>())).Returns(new List<Booking>());

            var result = _service.GetAvailableRooms(hotel.Id, 1, DateTime.Today, DateTime.Today.AddDays(1));

            result.Count.Should().Be(hotel.Rooms.Count);
        }

        [TestMethod]
        public void GetAvailableRooms_WhereBookingsClashWithSpecifiedStartDate_ReturnAvailableRooms()
        {
            var requestedStartDate = DateTime.Today.AddDays(2);
            var requestedEndDate = DateTime.Today.AddDays(3);
            var rooms = new List<Room>() { new RoomBuilder().WithId(21).WithCapacity(1).Build(), new RoomBuilder().WithId(22).WithCapacity(2).Build(), };
            var hotel = new Hotel { Id = 1, Name = "McKittrick", Rooms = rooms };
            var bookings = new List<Booking>()
            {
                new Booking {Hotel = hotel, Id = "b1", Name = "Madeline", PaymentReference="p1", Room = rooms[1], StartDate=requestedStartDate.AddDays(-1), EndDate=requestedStartDate, NumberOfPeople = 1},
            };

            _hotelRepository.Setup(r => r.GetHotel(It.IsAny<int>())).Returns(hotel);
            _roomRepository.Setup(r => r.GetRooms(It.IsAny<int>(), It.IsAny<int>())).Returns(rooms);
            _bookingRepository.Setup(r => r.GetBookings(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<List<Room>>())).Returns(bookings);

            var result = _service.GetAvailableRooms(hotel.Id, 1, requestedStartDate, requestedEndDate);

            result.Count.Should().Be(1);
        }

        [TestMethod]
        public void GetAvailableRooms_WhereBookingsClashWithSpecifiedEndDate_ReturnAvailableRooms()
        {
            var requestedStartDate = DateTime.Today.AddDays(2);
            var requestedEndDate = DateTime.Today.AddDays(3);
            var rooms = new List<Room>() { new RoomBuilder().WithId(21).WithCapacity(1).Build(), new RoomBuilder().WithId(22).WithCapacity(2).Build(), };
            var hotel = new Hotel { Id = 1, Name = "Overlook", Rooms = rooms };
            var bookings = new List<Booking>()
            {
                new Booking {Hotel = hotel, Id = "b1", Name = "Jack", PaymentReference="p1", Room = rooms[0], StartDate=requestedEndDate, EndDate=requestedEndDate.AddDays(2), NumberOfPeople = 1},
            };

            _hotelRepository.Setup(r => r.GetHotel(It.IsAny<int>())).Returns(hotel);
            _roomRepository.Setup(r => r.GetRooms(It.IsAny<int>(), It.IsAny<int>())).Returns(rooms);
            _bookingRepository.Setup(r => r.GetBookings(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<List<Room>>())).Returns(bookings);

            var result = _service.GetAvailableRooms(hotel.Id, 1, requestedStartDate, requestedEndDate);

            result.Count.Should().Be(1);
        }

        [TestMethod]
        public void MakeBooking_WhereAttemptToDoubleBookRoom_BookingRejected()
        {
            var requestedStartDate = DateTime.Today.AddDays(2);
            var requestedEndDate = DateTime.Today.AddDays(3);
            var rooms = new List<Room>() { new RoomBuilder().WithId(21).WithCapacity(1).Build(), new RoomBuilder().WithId(22).WithCapacity(2).Build(), };
            var hotel = new Hotel { Id = 1, Name = "Dolphin", Rooms = rooms };
            var bookedRoom = rooms[0];

            var bookings = new List<Booking>()
            {
                new Booking {Hotel = hotel, Id = "b1", Name = "John", PaymentReference="p1", Room = bookedRoom, StartDate=requestedEndDate, EndDate=requestedEndDate, NumberOfPeople = 1},
            };

            _hotelRepository.Setup(r => r.GetHotel(It.IsAny<int>())).Returns(hotel);
            _roomRepository.Setup(r => r.GetRooms(It.IsAny<int>(), It.IsAny<int>())).Returns(rooms);
            _bookingRepository.Setup(r => r.GetBookings(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<List<Room>>())).Returns(bookings);

            var request = new BookingRequest
            {
                CustomerName = "Bob",
                StartDate = requestedStartDate,
                EndDate = requestedEndDate,
                NumberOfPeople = 1,
                PaymentReference = "p2",
                HotelId = hotel.Id,
                RoomId = bookedRoom.Id
            };

            _service.Invoking(s => s.MakeBooking(request)).Should().Throw<Exception>();
        }

        [TestMethod]
        public void MakeBooking_WhereAttemptToBookUnoccupiedRoom_BookingAccepted()
        {
            var requestedStartDate = DateTime.Today.AddDays(2);
            var requestedEndDate = DateTime.Today.AddDays(3);
            var rooms = new List<Room>() { new RoomBuilder().WithId(21).WithCapacity(1).Build(), new RoomBuilder().WithId(22).WithCapacity(2).Build(), };
            var hotel = new Hotel { Id = 1, Name = "Sedgewick", Rooms = rooms };
            var bookedRoom = rooms[0];
            var availableRoom = rooms[1];

            var bookings = new List<Booking>()
            {
                new Booking {Hotel = hotel, Id = "b1", Name = "Peter", PaymentReference="p1", Room = bookedRoom, StartDate=requestedEndDate, EndDate=requestedEndDate, NumberOfPeople = 1},
            };

            _hotelRepository.Setup(r => r.GetHotel(It.IsAny<int>())).Returns(hotel);
            _roomRepository.Setup(r => r.GetRooms(It.IsAny<int>(), It.IsAny<int>())).Returns(rooms);
            _bookingRepository.Setup(r => r.GetBookings(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<List<Room>>())).Returns(bookings);

            var request = new BookingRequest
            {
                CustomerName = "Ray",
                StartDate = requestedStartDate,
                EndDate = requestedEndDate,
                NumberOfPeople = 1,
                PaymentReference = "p2",
                HotelId = hotel.Id,
                RoomId = availableRoom.Id
            };

            var result = _service.MakeBooking(request);

            result.Should().NotBeNull();
        }
    }
}
