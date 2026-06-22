using HotelManagement.Domain.Enums;

namespace HotelManagement.Application.DTOs.Booking;

public record GetUserBookingResult
(
 int BookingId,
 string GuestId,
 int RoomId,
 DateTime CheckIn,
 DateTime CheckOut,
 BookingStatus Status,
 decimal TotalPrice,
 DateTime CreatedAt
    );
