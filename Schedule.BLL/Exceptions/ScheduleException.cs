using System.Net;

namespace Schedule.BLL.Exceptions;

public class ScheduleException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ScheduleException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}