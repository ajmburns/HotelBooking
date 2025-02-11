using HotelBooking.Data;

namespace HotelBooking.Helpers
{
    public class BookingReferenceHelper
    {
        private const string StringPadFormat = "000";

        private const string DateFormat = "yyMMdd";

        /// <summary>
        /// Generate a unique booking reference from relevant properties (hotel, room, start/end dates).
        /// Customer details are not used.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string? GenerateBookingReference(BookingRequest request)
        {
            if (request != null)
            {
                return $"{request.HotelId.ToString(StringPadFormat)}-{request.RoomId.ToString(StringPadFormat)}-{request.StartDate.ToString(DateFormat)}-{request.EndDate.ToString(DateFormat)}";
            }

            return null;
        }
    }
}
