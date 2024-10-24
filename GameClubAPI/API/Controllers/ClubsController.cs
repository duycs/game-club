using Application.Models;
using Application.Services;
using Domain.Clubs;
using Infrastructure.Pagging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClubsController : ControllerBase
    {
        private readonly IGameClubService _gameClubService;
        private readonly ILogger<ClubsController> _logger;
        private readonly IUriService _uriService;

        public ClubsController(ILogger<ClubsController> logger,
            IUriService uriService,
            IGameClubService gameClubService)
        {
            _logger = logger;
            _uriService = uriService;
            _gameClubService = gameClubService;
        }

        /// <summary>
        /// Ping return Pong
        /// </summary>
        /// <returns>
        /// 1. Return 404 status: If missing in call url
        /// 2. Return 200 status: Return success
        /// </returns>
        [HttpGet("ping")]
        public IActionResult GetPing()
        {
            return Ok("pong");
        }


        /// <summary>
        /// A user can create a new game club by providing a unique club name and description.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// 1. Return 400 status: Require field
        /// 3. Return 409 status: Conflict if same name
        /// 3. Return 500 status: Require name or other internal error
        /// 4. Return 201 status: Return club have been created
        /// </returns>
        //[Authorize]
        [HttpPost]
        public IActionResult CreateClub([FromBody]CreateClubVM request)
        {
            if (!ModelState.IsValid) return BadRequest(new { Errors = ModelState });

            var existClub = _gameClubService.GetClubByName(request.Name);

            if (existClub != null)
            {
                return Conflict();
            }

            var club = _gameClubService.CreateClub(request);

            var location = Url.Action(nameof(CreateClub), new { id = club.Id }) ?? $"/{club.Id}";

            return Created(location, club);
        }

        /// <summary>
        /// 1. Not have any params: Retrieve a list of all game clubs, showing each club’s name and description
        /// 2. Have params: Retrieve a list of searched game clubs utilizing search params
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="searchValue"></param>
        /// <returns>
        /// 1. Return 500 status: If have internal error
        /// 2. Return 200 status:  Return result or empty list
        /// </returns>
        //[Authorize]
        [HttpGet]
        public IActionResult SearchClubs([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 0,
         [FromQuery] int id = 0, [FromQuery] string? name = "", [FromQuery] string? description = "",
         [FromQuery] string? text = "")
        {
            // pageNumber = 0 then return all
            var clubs = _gameClubService.SearchClubs(pageNumber, pageSize,
                id, name, description, text, out int totalRecords);

            return Ok(clubs);
        }

        /// <summary>
        /// A user can schedule an event for a specific club by providing a title description, and scheduled date/time
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>
        /// 1. Return 400 status: Require field
        /// 2. Return 409 status: Conflict if same club and title
        /// 3. Return 500 status: Require title or other internal error
        /// 4. Return 201 status: Return event have been created
        /// </returns>
        //[Authorize]
        [HttpPost("{id}/events")]
        public IActionResult CreateClubEvent(int id, [FromBody]CreateClubEventVM request)
        {
            if (!ModelState.IsValid) return BadRequest(new { Errors = ModelState });

            var existClubEvent = _gameClubService.GetClubEventByTitle(id, request.Title);
            if (existClubEvent != null)
            {
                return Conflict();
            }

            var clubEvent = _gameClubService.CreateClubEvent(id, request);

            var location = Url.Action(nameof(CreateClub), new { id = clubEvent.Id }) ?? $"{id}/events/{clubEvent.Id}";

            return Created(location, clubEvent);
        }

        /// <summary>
        /// Retrieve all events for a specific club, displaying the event title and scheduled date/time
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 1. Return 500 status: If have internal error
        /// 2. Return 200 status: Return result or empty list
        /// </returns>
        //[Authorize]
        [HttpGet("{id}/events")]
        public IActionResult GetClubEvents(int id)
        {
            var clubEvents = _gameClubService.GetClubEvents(id);
            return Ok(clubEvents);
        }

    }
}