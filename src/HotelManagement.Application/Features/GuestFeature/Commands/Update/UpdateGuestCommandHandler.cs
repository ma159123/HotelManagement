using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.GuestFeature.Commands.Update
{
    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, Result<string>>
    {
        private readonly IUserRepo _userRepo;
        public UpdateGuestCommandHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Result<string>> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            //check if profile exist
            var guest = await _userRepo.GetGuest(request.GuestId);
            if (guest == null)
            {
                return Result.Failure<string>(Error.NotFound);
            }

            //update
            if (request.FullName != null)
                guest.FullName = request.FullName;
            if (request.Address != null)
                guest.Address = request.Address;
            if (request.Phone != null)
                guest.Phone = request.Phone;

            await _userRepo.UpdateGuestAsync(guest);
            return Result.Success<string>("Updated");
        }
    }
}