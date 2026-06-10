using HotelManagement.Application.DTOs.Guest;
using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Mapping.User;
using HotelManagement.Domain.Common;
using HotelManagement.Domain.Common.Errors;
using MediatR;

namespace HotelManagement.Application.Features.GuestFeature.Queries.GetGuestById
{
    internal class GetGuestByIdQueryHandler : IRequestHandler<GetGuestByIdQuery, Result<GetGuestResponse>>
    {
        private readonly IUserRepo _userRepo;
        public GetGuestByIdQueryHandler(IUserRepo userRepo)
        {
            this._userRepo = userRepo;
        }
        public async Task<Result<GetGuestResponse>> Handle(GetGuestByIdQuery request, CancellationToken cancellationToken)
        {
            //get room
            var guest = _userRepo.GetGuestIncludeProfile(request.geustId);
            if (guest == null)
            {
                return Result.Failure<GetGuestResponse>(Error.NotFound);
            }
            //map list 
            var result = guest!.ToGuestResponse();
            // return
            return Result.Success<GetGuestResponse>(result);
        }
    }
}