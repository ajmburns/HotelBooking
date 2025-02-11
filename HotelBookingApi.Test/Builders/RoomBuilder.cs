using HotelBooking.Models;

namespace HotelBookingApi.Test.Builders
{
    internal class RoomBuilder
    {
        private int _id = 0;

        private int _capacity = 0;

        private RoomType _roomType = RoomType.Single;

        public RoomBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public RoomBuilder WithCapacity(int capacity)
        {
            _capacity = capacity;
            return this;
        }

        public Room Build()
        {
            return new Room
            {
                Id = _id,
                Capacity = _capacity,
                RoomType = _roomType,
                Description = "test room"
            };
        }
    }
}
