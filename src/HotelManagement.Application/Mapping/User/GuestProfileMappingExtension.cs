using HotelManagement.Application.Features.GuestFeature.GuestProfile.Commands.Update;
using HotelManagement.Application.Features.GuestFeature.GuestProfile.Create;
using HotelManagement.Domain.Entities;
using Mapster;

namespace HotelManagement.Application.Mapping.User
{
    public static class GuestProfileMappingExtension
    {
        static GuestProfileMappingExtension()
        {
            TypeAdapterConfig<CreateGuestProfileCommand, GuestProfile>
                .NewConfig()
                .Map(dest => dest.IdNumber, src => src.IdNumber)
                .Map(dest => dest.nationality, src => src.nationality);

            TypeAdapterConfig<UpdateGuestProfileCommand, GuestProfile>
               .NewConfig()
               .Map(dest => dest.IdNumber, src => src.IdNumber)
               .Map(dest => dest.nationality, src => src.nationality);


        }

        public static GuestProfile ToGuestProfile(this CreateGuestProfileCommand command)
        {
            return command.Adapt<GuestProfile>();
        }
        public static GuestProfile ToGuestProfile(this UpdateGuestProfileCommand command)
        {
            return command.Adapt<GuestProfile>();
        }

    }
}
