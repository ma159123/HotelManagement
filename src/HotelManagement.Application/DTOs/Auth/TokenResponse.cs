namespace HotelManagement.Application.DTOs.Auth;

public record TokenResponse(
string AccessToken,
string RefreshToken,
DateTime AccessTokenExpiry
);
