namespace HotelBooking.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required List<Room> Rooms { get; set; }
    }
}
