namespace Auth.Api
{
    using Auth.Api.Contracts.Models;
    using Auth.Api.Contracts.Services;
    using Auth.Api.Extensions;
    using Auth.Api.Models;
    using Auth.Api.Services;
    using Google.Cloud.Firestore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseApiKeyMiddleware();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var secretService = SecretService.Create().Result;
            var jwtService = new JwtService(secretService, this.Configuration.GetSection("Jwt").Get<AppSettingsJwt>());

            services.AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options => jwtService.SetOptions(options));

            services.AddControllers();

            services.AddOptions<AppSettings>().Bind(this.Configuration).ValidateDataAnnotations();
            services.AddSingleton<IAppSettings>(provider => provider.GetService<IOptions<AppSettings>>().Value);
            services.AddSingleton<CollectionReference>(
                provider =>
                {
                    var config = provider.GetService<IOptions<AppSettings>>().Value;
                    return FirestoreDb.Create(config.ProjectId).Collection(config.UserCollectionName);
                });
            services.AddSingleton<IDatabaseService, FirestoreDatabaseService>();
            services.AddSingleton<IJwtService>(jwtService);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IPasswordHashService, PasswordHashService>();
            services.AddControllers();
        }
    }
}
