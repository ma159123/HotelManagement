using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.GuestFeature.GuestProfile.Update
{
    public class UpdateGuestProfileCommandHandler : IRequestHandler<UpdateGuestProfileCommand, Result<string>>
    {
        private readonly IUserRepo _userRepo;
        public UpdateGuestProfileCommandHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Result<string>> Handle(UpdateGuestProfileCommand request, CancellationToken cancellationToken)
        {
            //check if profile exist
            var guestProfile = _userRepo.GetGuestProfile(request.ProfileId);
            if (guestProfile == null)
            {
                return Result.Failure<string>(Error.AlreadyElementExist);
            }
            //update
            if (request.nationality != null)
                guestProfile.nationality = request.nationality;
            if (request.DateOfBirth != null)
                guestProfile.DateOfBirth = request.DateOfBirth.Value;
            if (request.IdType != null)
                guestProfile.IdType = request.IdType.Value;

            _userRepo.UpdateGuestProfile(guestProfile);
            return Result.Success<string>("Updated");
        }
    }
}