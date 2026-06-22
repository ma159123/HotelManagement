using HotelManagement.Application.DTOs.Booking;
using HotelManagement.Application.Features.Bookings.Commands.CreateBooking;
using HotelManagement.Domain.Entities;
using Mapster;

namespace HotelManagement.Application.Mapping.Bookings
{
    public static class BookingsMappingExtensions
    {
        static BookingsMappingExtensions()
        {
            TypeAdapterConfig<CreateBookingCommand, Booking>
                .NewConfig()
                .Map(dest => dest.RoomId, src => src.RoomId);
            TypeAdapterConfig<Booking, GetUserBookingResult>
               .NewConfig()
               .Map(dest => dest.RoomId, src => src.RoomId);

        }

        public static Booking ToBooking(this CreateBookingCommand command)
        {
            return command.Adapt<Booking>();
        }
        public static List<GetUserBookingResult> ToBookingResultList(this List<Booking> bookings)
        {
            return bookings.Adapt<List<GetUserBookingResult>>();
        }

    }
}
