namespace HotelManagement.Application.DTOs.Auth;

public record RegisterResponse(
string GuestId,
string FullName,
string Email,
string Message
);
