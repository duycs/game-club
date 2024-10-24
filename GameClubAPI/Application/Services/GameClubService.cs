using Application.Models;
using Application.Queries;
using Domain.Clubs;
using Infrastructure.Repository;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class GameClubService : IGameClubService
    {
        private readonly ILogger<GameClubService> _logger;
        private readonly IRepositoryService _repositoryService;

        public GameClubService(ILogger<GameClubService> logger,
            IRepositoryService repositoryService
            )
        {
            _logger = logger;
            _repositoryService = repositoryService;
        }

        public Club CreateClub(CreateClubVM request)
        {
            var club = _repositoryService.Add(Club.Create(request.Name, request.Description));
            _repositoryService.SaveChanges();

            return club;
        }

        public IEnumerable<Club> GetClubs()
        {
            return _repositoryService.Find<Club>(new ClubSpecification(true));
        }

        public Club GetClubByName(string name)
        {
            return _repositoryService.Find<Club>(new ClubSpecification(true, 0, name)).FirstOrDefault();
        }

        public IEnumerable<Club> SearchClubs(int pageNumber, int pageSize, int id, string name, string description, string text, out int totalRecords)
        {
            var clubSpecification = new ClubSpecification(true, id, name, description, text);
            var clubs = _repositoryService.Find<Club>(pageNumber, pageSize, clubSpecification, out totalRecords);
            return clubs;
        }

        public Club GetClub(int id)
        {
            return _repositoryService.Find<Club>(id, new ClubSpecification(true));
        }

        public Event CreateClubEvent(int clubId, CreateClubEventVM request)
        {
            var clubEvent = _repositoryService.Add(Event.Create(clubId, request.Title, request.Description, request.Scheduled));
            _repositoryService.SaveChanges();
            return clubEvent;
        }

        public IEnumerable<Event> GetClubEvents(int clubId)
        {
            var club = GetClub(clubId);

            if (club == null) throw new NotFoundException("Club does not existing");

            if (club.Events == null)
            {
                return new List<Event>();
            }

            return club.Events;
        }

        public Event? GetClubEventByTitle(int clubId, string title)
        {
            var club = GetClub(clubId);

            if (club == null) throw new NotFoundException($"Club does not existing");

            if (club.Events != null)
            {
                var existEvent = club.Events.Where(e => e.Title.ToLower() == title.ToLower()).FirstOrDefault();
                return existEvent;
            }

            return null;
        }

    }
}
