using GoDate.API.Data;
using GoDate.API.Helpers;
using GoDate.API.Repositories.UserRepo;
using GoDate.API.Services.PhotoService;
using GoDate.API.Services.TokenService;
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

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            return services;
        }
    }
}
 