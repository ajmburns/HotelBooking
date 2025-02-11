using System.Text.Json.Serialization;

namespace HotelBooking.Models
{
    public class Room
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required RoomType RoomType { get; set; }

        public int Capacity { get; set; }

        public required string Description { get; set; }
    }
}
