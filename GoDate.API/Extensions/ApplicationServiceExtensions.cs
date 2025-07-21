using GoDate.API.Data;
using GoDate.API.Services;
using Microsoft.EntityFrameworkCore;

namespace GoDate.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
