using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.User;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.GuestFeature.GuestProfile.Create
{
    public class CreateGuestProfileHandler : IRequestHandler<CreateGuestProfileCommand, Result<string>>
    {
        private readonly IUserRepo _userRepo;
        public CreateGuestProfileHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Result<string>> Handle(CreateGuestProfileCommand request, CancellationToken cancellationToken)
        {
            //check if profile exist
            bool exist = _userRepo.IsGuestProfileExist(request.IdNumber);
            if (exist)
            {
                return Result.Failure<string>(Error.AlreadyElementExist);
            }
            //mapping 
            var guestProfile = request.ToGuestProfile();
            //create
            _userRepo.CreateGuestProfile(guestProfile);
            return Result.Success<string>("Created");
        }
    }
}
