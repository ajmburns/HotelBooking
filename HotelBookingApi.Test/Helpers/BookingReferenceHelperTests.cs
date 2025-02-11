using FluentAssertions;
using HotelBooking.Data;
using HotelBooking.Helpers;

namespace HotelBookingApi.Test.Helpers
{
    [TestClass]
    public class BookingReferenceHelperTests
    {
        [TestMethod]
        public void GenerateBookingReference_WhereAllDetailsSame_ReferencesAreIdentical()
        {
            var request1 = new BookingRequest { HotelId = 1, RoomId = 2, CustomerName = "Bob", NumberOfPeople = 2, PaymentReference = "p", StartDate = new DateTime(2025, 02, 10), EndDate = new DateTime(2025, 02, 11) };
            var request2 = new BookingRequest { HotelId = 1, RoomId = 2, CustomerName = "Bob", NumberOfPeople = 2, PaymentReference = "p", StartDate = new DateTime(2025, 02, 10), EndDate = new DateTime(2025, 02, 11) };

            var reference1 = BookingReferenceHelper.GenerateBookingReference(request1);
            var reference2 = BookingReferenceHelper.GenerateBookingReference(request2);

            reference1.Should().Be(reference2);
        }

        [TestMethod]
        public void GenerateBookingReference_WhereHotelRoomDiffers_ReferenceDiffers()
        {
            var request1 = new BookingRequest { HotelId = 1, RoomId = 2, CustomerName = "Bob", NumberOfPeople = 2, PaymentReference = "p", StartDate = new DateTime(2025, 02, 10), EndDate = new DateTime(2025, 02, 11) };
            var request2 = new BookingRequest { HotelId = 3, RoomId = 4, CustomerName = "Bob", NumberOfPeople = 2, PaymentReference = "p", StartDate = new DateTime(2025, 02, 10), EndDate = new DateTime(2025, 02, 11) };

            var reference1 = BookingReferenceHelper.GenerateBookingReference(request1);
            var reference2 = BookingReferenceHelper.GenerateBookingReference(request2);

            reference1.Should().NotBe(reference2);
        }

        [TestMethod]
        public void GenerateBookingReference_WhereCustomerNameDiffers_ReferencesAreIdentical()
        {
            var request1 = new BookingRequest { HotelId = 1, RoomId = 2, CustomerName = "Bob", NumberOfPeople = 2, PaymentReference = "p", StartDate = new DateTime(2025, 02, 10), EndDate = new DateTime(2025, 02, 11) };
            var request2 = new BookingRequest { HotelId = 1, RoomId = 2, CustomerName = "Jim", NumberOfPeople = 2, PaymentReference = "p", StartDate = new DateTime(2025, 02, 10), EndDate = new DateTime(2025, 02, 11) };

            var reference1 = BookingReferenceHelper.GenerateBookingReference(request1);
            var reference2 = BookingReferenceHelper.GenerateBookingReference(request2);

            reference1.Should().Be(reference2);
        }

        [TestMethod]
        public void GenerateBookingReference_WhereDateRangeDiffers_ReferenceDiffers()
        {
            var request1 = new BookingRequest { HotelId = 1, RoomId = 2, CustomerName = "Bob", NumberOfPeople = 2, PaymentReference = "p", StartDate = new DateTime(2025, 02, 10), EndDate = new DateTime(2025, 02, 11) };
            var request2 = new BookingRequest { HotelId = 1, RoomId = 2, CustomerName = "Bob", NumberOfPeople = 2, PaymentReference = "p", StartDate = new DateTime(2025, 02, 20), EndDate = new DateTime(2025, 02, 21) };

            var reference1 = BookingReferenceHelper.GenerateBookingReference(request1);
            var reference2 = BookingReferenceHelper.GenerateBookingReference(request2);

            reference1.Should().NotBe(reference2);
        }
    }
}
