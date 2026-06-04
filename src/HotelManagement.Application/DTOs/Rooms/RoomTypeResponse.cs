namespace HotelManagement.Application.Features.Rooms.DTOs;

public record RoomTypeResponse(
         int RoomTypeId,
         string Name,
         string? Description,
         int Capacity,
         decimal BasePrice,
         string? Amenities
       );
