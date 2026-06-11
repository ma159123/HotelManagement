using Microsoft.AspNetCore.Http;

namespace HotelManagement.Domain.Common.Errors;

public record Error(string Code, string Description, int? StatusCode)
{
    public static readonly Error None = new(string.Empty, string.Empty, null);

    public static readonly Error PageOutOfRange = new(
        "Pagination.PageOutOfRange",
        "The requested page number is out of range.",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error DbError = new(
        "Database.Error",
        "An error occurred while accessing the database.",
        StatusCodes.Status500InternalServerError
    );
    public static readonly Error AlreadyElementExist = new(
      "Found.AlreadyElementExist",
      "Already Element Exist!",
      StatusCodes.Status400BadRequest
  );
    public static readonly Error NotFound = new(
     "Found.NotFound",
     "Not Found",
     StatusCodes.Status404NotFound
 );
    public static readonly Error AlreadyEmailConfirmed = new(
  "Confirmation.AlreadyEmailConfirmed",
  "Already email confirmed",
  StatusCodes.Status400BadRequest
);
    public static readonly Error TooManyRequests = new(
"Auth.TooManyRequests",
"Too many requests",
StatusCodes.Status400BadRequest
);
}
