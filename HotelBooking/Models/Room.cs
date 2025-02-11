namespace HotelBooking.Models
{
    public class Room
    {
        public int Id { get; set; }

        public required RoomType RoomType { get; set; }

        public int Capacity { get; set; }

        public required string Description { get; set; }
    }
}
