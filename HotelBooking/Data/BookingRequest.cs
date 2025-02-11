namespace HotelBooking.Data
{
    public class BookingRequest
    {
        public int HotelId { get; set; }

        public int RoomId { get; set; }

        public int NumberOfPeople { get; set; }

        public required string CustomerName { get; set; }

        public required string PaymentReference { get; set; }

        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; }

        /// <summary>
        /// Validate the request has all required data.
        /// Does not verify whether the requested room is available.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return HotelId > 0
                    && RoomId > 0
                    && NumberOfPeople > 0
                    && !String.IsNullOrWhiteSpace(CustomerName)
                    && !String.IsNullOrWhiteSpace(PaymentReference)
                    && StartDate.Date >= DateTime.Today.Date
                    && StartDate.Date <= EndDate.Date;
            }
        }
    }
}
