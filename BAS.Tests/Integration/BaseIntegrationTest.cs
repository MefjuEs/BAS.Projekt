using BAS.Identity;
using BAS.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using BAS.AppServices;
using Xunit;
using BAS.Tests.Services;
using System.IO;

namespace BAS.Tests.Integration
{
    public class IntegrationTestsFixture
    {
        private IServiceProvider serviceProvider;
        private AppConfig config;

        public IServiceProvider ServiceProvider => this.serviceProvider;
        public AppConfig Config => this.config;

        public IntegrationTestsFixture()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(StaticValues.SettingsFileName)
                .Build();
            var testsConfig = new AppConfig();
            ConfigurationBinder.Bind(configuration, testsConfig);

            this.config = testsConfig;
            this.serviceProvider = this.Setup();
        }

        private IServiceProvider Setup()
        {
            var services = new ServiceCollection();

            this.ConfigureDatabase(services);
            this.RegisterServices(services);

            var serviceProvider = services.BuildServiceProvider();
            this.CreateIdentityDatabase(serviceProvider);
            this.CreateMovieDatabase(serviceProvider);
            this.ClearContent(serviceProvider);

            return serviceProvider;
        }

        private void ClearContent(IServiceProvider serviceProvider)
        {
            var appContext = serviceProvider.GetRequiredService<IAppContext>();

            if (!Directory.Exists(appContext.ServableContentPath))
            {
                Directory.CreateDirectory(appContext.ServableContentPath);
            }

            var directoryInfo = new DirectoryInfo(appContext.ServableContentPath);
           
            foreach (var file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (var dir in directoryInfo.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options => { options.UseSqlServer(this.config.ConnectionStrings.Identity); });
            services.AddDbContext<MovieDbContext>(options => { options.UseSqlServer(this.config.ConnectionStrings.MovieDatabase); });
            services.AddIdentity<ApplicationUser, IdentityRole<long>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddOptions();
            services.AddSingleton(x => this.config);
            services.AddSingleton<IAppContext>(x => new TestsAppContext());
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPersonnelService, PersonnelService>();
        }

        private void CreateIdentityDatabase(IServiceProvider serviceProvider)
        {
            var identityContext = serviceProvider.GetService<IdentityContext>();
            identityContext.Database.Migrate();
        }

        private void CreateMovieDatabase(IServiceProvider serviceProvider)
        {
            var movieContext = serviceProvider.GetService<MovieDbContext>();
            movieContext.Database.Migrate();
        }
    }

    public class BaseIntegrationTest : IClassFixture<IntegrationTestsFixture>
    {
        protected readonly IServiceProvider serviceProvider;
        protected static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public BaseIntegrationTest(IntegrationTestsFixture services)
        {
            this.serviceProvider = services.ServiceProvider;
            this.ClearIdentityDatabase(serviceProvider);
            this.ClearMovieDatabase(serviceProvider);
        }

        private void ClearIdentityDatabase(IServiceProvider serviceProvider)
        {
            var identityContext = serviceProvider.GetService<IdentityContext>();
            identityContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[AspNetUserClaims]");
            identityContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[AspNetUserLogins]");
            identityContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[AspNetUserRoles]");
            identityContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[AspNetUserTokens]");
            identityContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[AspNetUsers]");
            identityContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[AspNetRoleClaims]");
            identityContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[AspNetRoles]");
        }
 
        private void ClearMovieDatabase(IServiceProvider serviceProvider)
        {
            var movieContext = serviceProvider.GetService<MovieDbContext>();
            movieContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[MovieGenres]");
            movieContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[Genres]");
            movieContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[MoviePersonnel]");
            movieContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[Actors]");
            movieContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[Reviews]");
            movieContext.Database.ExecuteSqlRaw($"DELETE FROM [dbo].[Movies]");
        }
    }
}
