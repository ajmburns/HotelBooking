using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required string Id { get; set; }

        public required string PaymentReference { get; set; }

        public required string Name { get; set; }

        public virtual required Hotel Hotel { get; set; }

        public virtual required Room Room { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int NumberOfPeople { get; set; }
    }
}
