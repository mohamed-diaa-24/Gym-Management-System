namespace GymManagementSystem.Extensions
{
    public static class SessionServiceExtensions
    {
        public static IServiceCollection AddSessionServices(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            return services;
        }
    }
}