namespace GradeCenter.Server.Web
{
    using System.Reflection;

    using GradeCenter.Server.Services.Mapping;
    using GradeCenter.Server.Web.Infrastructure;
    using GradeCenter.Server.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDatabase(this.configuration)
                .AddIdentity()
                .AddApplicationServices()
                .AddSwagger()
                .AddControllers();

            // Keep if env == development
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            app
                .SeedInitialDataOnStartup()
                .UseExceptionHandling(env)
                .UserSwaggerUI()
                .UseRouting()
                .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints();
        }
    }
}
