using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.Aminity.Commands.Update
{
    internal class UpdateAminityHandler : IRequestHandler<UpdateAminityCommand, Result<string>>
    {
        private readonly IAmenityRepo _amenityRepo;
        public UpdateAminityHandler(IAmenityRepo amenityRepo)
        {
            this._amenityRepo = amenityRepo;
        }
        public async Task<Result<string>> Handle(UpdateAminityCommand request, CancellationToken cancellationToken)
        {
            // 1. Check if room exists
            var amenity = await _amenityRepo.GetAmenityById(request.AmenityId);
            if (amenity == null)
                return Result.Failure<string>(Error.NotFound);

            // 2. Update properties (manual or mapping)

            if (!string.IsNullOrEmpty(request.Description))
                amenity.Description = request.Description;

            if (!string.IsNullOrEmpty(request.Name))
                amenity.Name = request.Name;

            if (request.IconUrl != null)
                amenity.IconUrl = request.IconUrl;

            // 3. Save
            _amenityRepo.UpdateAmenity(amenity);
            await _amenityRepo.saveChangesAsync();

            return Result.Success("Updated");
        }
    }
}
