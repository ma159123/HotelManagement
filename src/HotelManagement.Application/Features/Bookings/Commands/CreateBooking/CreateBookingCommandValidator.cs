using FluentValidation;

namespace HotelManagement.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.RoomId).NotNull().WithMessage("RoomId required.")
                .GreaterThan(0)
                .WithMessage("RoomId must be greater than 0.");

            RuleFor(x => x.CheckIn)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("CheckIn must be in the future.");

            RuleFor(x => x.CheckOut)
                .GreaterThan(x => x.CheckIn)
                .WithMessage("CheckOut must be after CheckIn.");

            RuleFor(x => x)
             .Must(x => (x.CheckOut - x.CheckIn).TotalDays >= 1)
             .WithMessage("Booking must be at least 1 night.");
        }
    }
}
