using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAloneBackend.Lib
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyDbContext<T>(
            this IServiceCollection services,
            IConfiguration configuration) where T : DbContext
        {
            return services.AddDbContext<T>(options =>
            {
                var nameOfT = typeof(T).Name;
                var connectionString = configuration.GetConnectionString(nameOfT);
                options.UseSqlServer(connectionString);
            });
        }
    }
}
