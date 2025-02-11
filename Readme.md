# HotelBooking API

Provides a simple example API for searching/booking hotel rooms.

Not implemented:
* Authentication
* Payment process (or room reservation pending payment confirmation)

Rooms can only be booked if available (with sufficient capacity) for all the selected date(s).

Admin endpoints provided for initialising/clearing data.

## Instructions

Launch solution in Visual Studio - will create a simple MSSqlLocalDB.

Swagger API docs provide full details of avaialable endpoints.

Populate with hotel data using /HotelAdmin endpoint and Json list of hotels.
All hotels created with same six room configuration.
Data can be cleared using /HotelAdmin DELETE endpoint.

Search hotels by name (or part of) using /HotelSearch

To book a room:
1. Query available rooms using /RoomSearch
1. Query/create bookings using /Booking
1. Payment note: assumes an external payment process exists to take payment and produce a payment reference
1. A booking reference is generated for the hotel+room+dates selected


## Example Setup Data

```
{
  "hotels": [
    {"name": "Chateau Marmont"},
    {"name": "Empire Hotel"},
    {"name": "Plaza Hotel"},
    {"name": "Overlook Hotel"},
    {"name": "Continental Hotel"},
    {"name": "McKittrick Hotel"},
    {"name": "Pinewood Motel"},
    {"name": "Dolphin Hotel"},
    {"name": "Desert Valley Motel"},
    {"name": "Desert Inn"},
    {"name": "Sedgewick Hotel"}
  ]
}
```