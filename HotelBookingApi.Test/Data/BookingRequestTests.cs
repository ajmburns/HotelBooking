using FluentAssertions;
using HotelBooking.Data;

namespace HotelBookingApi.Test.Data
{
    [TestClass]
    public class BookingRequestTests
    {
        [TestMethod]
        public void IsValid_WhereAllDetailsSupplied_IsValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                RoomId = 2,
                NumberOfPeople = 3,
                CustomerName = "Bob",
                PaymentReference = "p3",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
            };

            request.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_WhereHotelMissing_IsInValid()
        {
            var request = new BookingRequest
            {
                RoomId = 2,
                NumberOfPeople = 3,
                CustomerName = "Bob",
                PaymentReference = "p3",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
            };

            request.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WhereRoomMissing_IsInValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                NumberOfPeople = 3,
                CustomerName = "Bob",
                PaymentReference = "p3",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
            };

            request.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WhereNumberOfPeopleMissing_IsInValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                RoomId = 2,
                CustomerName = "Bob",
                PaymentReference = "p3",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
            };

            request.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WhereCustomerNameMissing_IsInValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                RoomId = 2,
                NumberOfPeople = 3,
                CustomerName = string.Empty,
                PaymentReference = "p3",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
            };

            request.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WherePaymentReferenceMissing_IsInValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                RoomId = 2,
                NumberOfPeople = 3,
                CustomerName = "Bob",
                PaymentReference = string.Empty,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
            };

            request.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WhereDateWindowReversed_IsInValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                RoomId = 2,
                NumberOfPeople = 3,
                CustomerName = "Bob",
                PaymentReference = "p3",
                StartDate = DateTime.Today.AddDays(2),
                EndDate = DateTime.Today,
            };

            request.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WhereStartDateInPast_IsInValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                RoomId = 2,
                NumberOfPeople = 3,
                CustomerName = "Bob",
                PaymentReference = "p3",
                StartDate = DateTime.Today.AddDays(-2),
                EndDate = DateTime.Today.AddDays(3),
            };

            request.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WhereDateRangeIsSingleDay_IsValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                RoomId = 2,
                NumberOfPeople = 3,
                CustomerName = "Bob",
                PaymentReference = "p3",
                StartDate = DateTime.Today.AddDays(2),
                EndDate = DateTime.Today.AddDays(2),
            };

            request.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_WhereDateRangeIsSingleDayIgnoreHours_IsValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                RoomId = 2,
                NumberOfPeople = 3,
                CustomerName = "Bob",
                PaymentReference = "p3",
                StartDate = DateTime.Today.AddDays(2).AddHours(7),
                EndDate = DateTime.Today.AddDays(2).AddHours(3),
            };

            request.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_WhereDateRangeIsMultipleDays_IsValid()
        {
            var request = new BookingRequest
            {
                HotelId = 1,
                RoomId = 2,
                NumberOfPeople = 3,
                CustomerName = "Bob",
                PaymentReference = "p3",
                StartDate = DateTime.Today.AddDays(2),
                EndDate = DateTime.Today.AddDays(7),
            };

            request.IsValid.Should().BeTrue();
        }
    }
}
