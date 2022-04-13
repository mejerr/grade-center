namespace GradeCenter.Server.Web.Infrastructure
{
    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContext<GradeCenterDbContext>(options => options
                    .UseSqlServer(configuration.GetDefaultConnectionString()));

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<GradeCenterDbContext>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            /*
             Application services
            services.AddTransient<ISettingsService, SettingsService>();
            */

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "GradeCenter API",
                        Version = "v1",
                    });
            });

            return services;
        }
    }
}
