using System.Net;

namespace Schedule.BLL.Exceptions;

public class NotFoundException : ScheduleException
{
    public NotFoundException(string entityName, int id)
        : base($"{entityName} з id={id} не знайдено.", HttpStatusCode.NotFound)
    {
    }
}