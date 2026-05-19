using System.Net;

namespace Schedule.BLL.Exceptions;

public class ConflictException : ScheduleException
{
    public ConflictException(string message)
        : base(message, HttpStatusCode.Conflict)
    {
    }
}