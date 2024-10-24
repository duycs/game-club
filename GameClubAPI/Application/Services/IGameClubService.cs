using Application.Models;
using Domain.Clubs;

namespace Application.Services
{
    public interface IGameClubService
    {
        Club CreateClub(CreateClubVM request);
        Club? GetClub(int id);
        IEnumerable<Club> GetClubs();
        IEnumerable<Club> SearchClubs(int pageNumber, int pageSize, int id, string name, string description, string text, out int totalRecords);
        Event CreateClubEvent(int clubId, CreateClubEventVM request);
        IEnumerable<Event> GetClubEvents(int clubId);
    }       
}
