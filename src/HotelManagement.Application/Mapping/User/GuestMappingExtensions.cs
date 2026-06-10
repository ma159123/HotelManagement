using HotelManagement.Application.DTOs.Guest;
using HotelManagement.Domain.Entities;
using Mapster;

namespace HotelManagement.Application.Mapping.User
{
    public static class GuestMappingExtensions
    {
        static GuestMappingExtensions()
        {
            TypeAdapterConfig<Guest, GetGuestResponse>
                .NewConfig()
                .Map(dest => dest.IdNumber, src => src.Profile.IdNumber)
                .Map(dest => dest.nationality, src => src.Profile.nationality)
            .Map(dest => dest.DateOfBirth, src => src.Profile.DateOfBirth);

        }

        public static GetGuestResponse ToGuestResponse(this Guest guest)
        {
            return guest.Adapt<GetGuestResponse>();
        }

    }
}
