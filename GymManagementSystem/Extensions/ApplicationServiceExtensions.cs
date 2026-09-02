using GymManagementSystem.Services.Implementations;
using GymManagementSystem.Services.Interfaces;

namespace GymManagementSystem.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<IGymClassService, GymClassService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();

        return services;
    }
}