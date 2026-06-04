using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.Amenities;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.Aminity.Commands.Create
{
    public class CreateAminityHandler : IRequestHandler<CreateAminityCommand, Result<string>>
    {
        private readonly IAmenityRepo _amenityRepo;
        public CreateAminityHandler(IAmenityRepo amenityRepo)
        {
            this._amenityRepo = amenityRepo;
        }
        public async Task<Result<string>> Handle(CreateAminityCommand request, CancellationToken cancellationToken)
        {
            //check if room exist
            bool isExist = await _amenityRepo.IsAmenityExist(request.Name);
            if (isExist)
            {
                return Result.Failure<string>(Error.AlreadyElementExist);
            }
            //mapping
            var amenity = request.CreateAmenityCommandToAmenity();
            _amenityRepo.CreateAmenity(amenity);
            await _amenityRepo.saveChangesAsync();
            return Result.Success<string>("Created");
        }
    }
}
