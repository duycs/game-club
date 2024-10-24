using Application.Models;
using Application.Services;
using Infrastructure.Models;
using Infrastructure.Pagging;
using Infrastructure.Persistences;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CrossCutting
{
    public static class InjectorBootStrapper
    {
        public static void AddLayersInjector(this IServiceCollection services, IConfiguration configuration)
        {
            // Infrastructure
            // DataAccess in memory or relation database
            var useInMemoryDb = configuration.GetValue<bool>("UseInMemoryDb");
            //var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS");
            var connectionString = configuration.GetConnectionString("GameClub");

            if (useInMemoryDb)
            {
                services.AddDbContext<GameClubContext>(options => options.UseInMemoryDatabase(configuration.GetConnectionString("Default")));
            }
            else
            {
                // Mysql provider
                //services.AddDbContext<GameClubContext>(options => options.UseMySQL(connectionString,
                //    options => options.CommandTimeout(300)));

                services.AddDbContext<GameClubContext>(options => options.UseSqlite(connectionString,
                options => options.CommandTimeout(300)));
            }

            // Persistence: generic repository
            services.AddTransient<IDatabaseService, GameClubContext>();
            services.AddScoped<IRepositoryService, RepositoryService>();

            // Application: service usecase
            services.AddScoped<IGameClubService, GameClubService>();

            // Present
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUriService>(o =>
            {
                var request = o.GetRequiredService<IHttpContextAccessor>().HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

            // Logger
            services.AddSingleton<ILoggerFactory, LoggerFactory>();

        }
    }
}