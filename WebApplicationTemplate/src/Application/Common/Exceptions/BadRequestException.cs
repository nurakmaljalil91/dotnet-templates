using Domain.Common;
using System.Net;

namespace Application.Common.Exceptions;

public abstract class BadRequestException : Exception
{
    public BaseResponse ErrorResponse { get; set; }
    public BadRequestException()
           : base()
    {
    }

    public BadRequestException(string details)
          : base()
    {
        ErrorResponse = new BaseResponse
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Message = "Bad Request",
            Details = details
        };
    }
}
