using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schedule.BLL.Mapping;
using Schedule.BLL.Services;
using Schedule.BLL.Services.Interfaces;
using Schedule.DAL.Context;
using Schedule.DAL.Repositories;
using Schedule.DAL.Repositories.Interfaces;

namespace Schedule.BLL.Extensions;

public static class BllExtensions 
{
    public static IServiceCollection AddBusinessLayer(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ScheduleContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddAutoMapper(cfg => cfg.AddProfile<ScheduleMappingProfile>());
        services.AddScoped<IScheduleService, ScheduleService>();

        return services;
    }
}