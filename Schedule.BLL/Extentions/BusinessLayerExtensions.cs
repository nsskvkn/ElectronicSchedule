using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schedule.DAL.Context;

namespace Schedule.BLL.Extensions;

public static class BusinessLayerExtensions
{
    public static IServiceCollection AddBusinessLayer(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ScheduleContext>(options =>
            options.UseSqlServer(connectionString));

        // Ви два чуда повинні сюди додати реєстацію сервісів та репозиторіїв

        return services;
    }
}