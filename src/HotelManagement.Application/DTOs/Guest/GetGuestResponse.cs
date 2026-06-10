namespace HotelManagement.Application.DTOs.Guest;

public record GetGuestResponse
(
 string GuestId,
 string FullName,
 string email,
 string? Phone,
 string? Address,
 string IdNumber,
 string nationality,
 DateTime DateOfBirth
    );
