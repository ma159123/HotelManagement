using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.Aminity.Commands.Delete
{
    public class DeleteAminityHandler : IRequestHandler<DeleteAminityCommand, Result<string>>
    {
        private readonly IAmenityRepo _amenityRepo;
        public DeleteAminityHandler(IAmenityRepo amenityRepo)
        {
            this._amenityRepo = amenityRepo;
        }
        public async Task<Result<string>> Handle(DeleteAminityCommand request, CancellationToken cancellationToken)
        {
            //check if room exist
            var amenity = await _amenityRepo.GetAmenityById(request.Id);
            if (amenity == null)
            {
                return Result.Failure<string>(Error.NotFound);
            }
            //mapping
            _amenityRepo.DeleteAmenity(amenity);
            await _amenityRepo.saveChangesAsync();
            return Result.Success<string>("Deleted");
        }
    }
}
