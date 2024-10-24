using CrossCutting;
using Domain.Clubs;
using Infrastructure.Extensions;
using Infrastructure.Persistences;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace Testing
{
    public class TestBase
    {
        public IConfiguration _configuration { get; }
        public IServiceCollection _serviceCollection { get; }
        public IServiceProvider _serviceProvider { get; set; }
        public IRepositoryService _repositoryService { get; set; }
        public GameClubContext _gameClubContext { get; set; }

        public TestBase()
        {
            // Set appsetting config file
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            _configuration = configuration;
            var useInMemoryDb = configuration.GetValue<bool>("UseInMemoryDb");

            // Inject services, database in memory
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddLayersInjector(configuration);

            // Then create serviceProvider
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _repositoryService = _serviceProvider.GetService<IRepositoryService>();
            //_GameClubContext = _serviceProvider.GetRequiredService<GameClubContext>();

            // init master data
            if (useInMemoryDb)
            {
                Init_Club_Event_Data();
            }
        }

        private void Init_Club_Event_Data()
        {
            // club
            _repositoryService.Add(new[]
            {
                Club.Create(StringExtension.RandomString(), "Club description"),
                Club.Create(StringExtension.RandomString(), "Club description"),
                Club.Create(StringExtension.RandomString(), "Club description"),
              });
            
            _repositoryService.SaveChanges();

            // event
            _repositoryService.Add(new[] {
                Event.Create(1, StringExtension.RandomString(), "event description", new DateTime()),
                Event.Create(1, StringExtension.RandomString(), "event description", new DateTime())
            });

            _repositoryService.SaveChanges();
        }
    }
}
