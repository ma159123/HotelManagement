namespace HotelManagement.Application.DTOs.Auth;

public record LoginResponse(
string GuestId,
string FullName,
string Email,
string AccessToken,
string RefreshToken,
DateTime AccessTokenExpiry
);
