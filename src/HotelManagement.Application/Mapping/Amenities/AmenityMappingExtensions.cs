using HotelManagement.Application.Features.Aminity.Commands.Create;
using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Domain.Entities;
using Mapster;

namespace HotelManagement.Application.Mapping.Amenities
{
    public static class AmenityMappingExtensions
    {
        static AmenityMappingExtensions()
        {
            TypeAdapterConfig<Amenity, AmenityResponse>
                .NewConfig();
        }
        public static AmenityResponse ToResponse(this Amenity amenity)
        {
            return amenity.Adapt<AmenityResponse>();
        }
        public static List<AmenityResponse> ToResponseList(this List<Amenity> amenities)
        {
            return amenities.Adapt<List<AmenityResponse>>();
        }
        public static Amenity CreateAmenityCommandToAmenity(this CreateAminityCommand amenityCommand)
        {
            return amenityCommand.Adapt<Amenity>();
        }
    }
}
