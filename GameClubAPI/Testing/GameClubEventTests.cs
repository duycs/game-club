using Application.Models;
using Application.Services;
using Domain;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Testing
{
    public class JobServiceTests : TestBase
    {
        private IGameClubService _gameClubService;

        [OneTimeSetUp]
        public void Init()
        {
            _gameClubService = _serviceProvider.GetService<IGameClubService>();
        }

        [TearDown]
        public void Clean()
        {
            //_GameClubContext.Database.EnsureDeleted();
            //_GameClubContext.Dispose();
        }

        [Test]
        public void Create_New_Club_Success()
        {
            // arrange
            var createClubVM = new CreateClubVM()
            {
                Name = StringExtension.RandomString(),
                Description = "Club description"
            };

            // act
            var clubCreated = _gameClubService.CreateClub(createClubVM);

            // assert
            Assert.IsNotNull(clubCreated);
            Assert.NotZero(clubCreated.Id);
        }
        
    }
}
