// HotelManagement.Application/Mapping/Rooms/RoomMappingExtensions.cs
using HotelManagement.Application.Features.Rooms.Commands.Room.Create;
using HotelManagement.Application.Features.Rooms.DTOs;
using HotelManagement.Domain.Entities;
using Mapster;

namespace HotelManagement.Application.Mapping.Rooms;

public static class RoomMappingExtensions
{
    // Static constructor - runs once
    static RoomMappingExtensions()
    {
        TypeAdapterConfig<Room, RoomResponse>
            .NewConfig()
            .Map(dest => dest.RoomTypeName, src => src.RoomType.Name)
            .Map(dest => dest.Status, src => src.Status.ToString());
        TypeAdapterConfig<CreateRoomCommand, Room>
           .NewConfig()
           .Map(dest => dest.ImageUrl, src => src.ImageUrl)
           .Map(dest => dest.Status, src => src.Status)
        .Map(dest => dest.PricePerNight, src => src.PricePerNight);

        TypeAdapterConfig<CreateRoomTypeCommand, RoomType>
         .NewConfig()
         .Map(dest => dest.Name, src => src.Name);
    }

    public static RoomResponse ToResponse(this Room room)
    {
        return room.Adapt<RoomResponse>();
    }

    public static List<RoomResponse> ToResponseList(this List<Room> rooms)
    {
        return rooms.Adapt<List<RoomResponse>>();
    }
    public static Room CreateRoomCommandToRoom(this CreateRoomCommand roomCommand)
    {
        return roomCommand.Adapt<Room>();
    }

    public static RoomType CreateRoomTypeCommandToRoomType(this CreateRoomTypeCommand roomTypeCommand)
    {
        return roomTypeCommand.Adapt<RoomType>();
    }
}