using FetPamily.Application.Volunteers.CreateVolunteer;
using Microsoft.Extensions.DependencyInjection;

namespace FetPamily.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        
        return services;
    }
}