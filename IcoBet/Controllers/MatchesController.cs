namespace IcoBet.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    using IcoBet.Web.ViewModels.Match;
    using IcoBet.Services.Mapping;
    using IcoBet.Services.Data.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService matchService;
        private readonly IMappingService mappingService;

        public MatchesController(IMatchService matchService, IMappingService mappingService)
        {
            this.matchService = matchService;
            this.mappingService = mappingService;
        }

        // GET: api/<MatchesController>
        [HttpGet]
        public IEnumerable<MatchViewModel> Get()
        {
            var matches = this.matchService.GetMatchesForTheNextTwentyFourHours();

            var viewMatches = this.mappingService.MapMatchServiceModelsToMatchViewModels(matches);

            return viewMatches;
        }

        // GET api/<MatchesController>/5
        [HttpGet("{id}")]
        public MatchViewModel Get(string id)
        {
            var match = this.matchService.GetMatch(id);

            var viewMatch = this.mappingService.MapMatchServiceModelToMatchViewModel(match);

            return viewMatch;
        }
    }
}
