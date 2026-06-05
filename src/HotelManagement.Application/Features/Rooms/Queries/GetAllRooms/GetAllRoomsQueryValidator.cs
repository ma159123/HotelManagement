using FluentValidation;

namespace HotelManagement.Application.Features.Rooms.Queries.GetAllRooms
{
    public class GetAllRoomsQueryValidator : AbstractValidator<GetAllRoomsQuery>
    {
        public GetAllRoomsQueryValidator()
        {
            #region Pagination

            RuleFor(x => x.page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page must be at least 1.");

            RuleFor(x => x.pageSize)
                .InclusiveBetween(1, 50)
                .WithMessage("PageSize must be between 1 and 50.");

            #endregion

            #region Filters

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MinPrice must be greater than or equal to 0.")
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MaxPrice must be greater than or equal to 0.")
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x)
                .Must(x => x.MinPrice <= x.MaxPrice)
                .WithMessage("MinPrice must be less than or equal to MaxPrice.")
                .When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue);

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid Status value.")
                .When(x => x.Status.HasValue);

            #endregion

            #region Sort

            #endregion

        }
    }
}
