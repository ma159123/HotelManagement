namespace HotelManagement.Application.Features.Rooms.DTOs;

public record RoomResponse(
    int RoomId,
    string RoomNumber,
    string RoomTypeName,
    int Floor,
    string ViewType,
    string Status,
    decimal PricePerNight,
    string? ImageUrl
);
