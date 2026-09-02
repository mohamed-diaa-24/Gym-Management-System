using GymManagementSystem.Repositories.Implementations;
using GymManagementSystem.Repositories.Interfaces;

namespace GymManagementSystem.Extensions;

public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<IGymClassRepository, GymClassRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

        return services;
    }
}