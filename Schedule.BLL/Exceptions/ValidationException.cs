using System.Net;

namespace Schedule.BLL.Exceptions;

public class ValidationException : ScheduleException
{
    public ValidationException(string message)
        : base(message, HttpStatusCode.BadRequest)
    {
    }
}