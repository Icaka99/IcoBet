namespace IcoBet.Services.Mapping
{
    using System.Linq;
    using System.Collections.Generic;

    using IcoBet.Web.ViewModels.Match;
    using IcoBet.Web.ViewModels.Odd;
    using IcoBet.Web.ViewModels.Bet;
    using IcoBet.Services.Models.ServiceModels;

    public class MappingService : IMappingService
    {
        public IEnumerable<MatchViewModel> MapMatchServiceModelsToMatchViewModels(IEnumerable<MatchServiceModel> model)
        {
            return model.Select(x => new MatchViewModel
            {
                ID = x.ID,
                Name = x.Name,
                StartDate = x.StartDate,
                EventId = x.EventId,
                MatchType = x.MatchType,
                Bets = x.Bets.Select(bet => new BetViewModel
                {
                    ID = bet.ID,
                    Name = bet.Name,
                    IsLive = bet.IsLive,
                    MatchId = bet.MatchId,
                    Odds = bet.Odds.Select(odd => new OddViewModel
                    {
                        ID = odd.ID,
                        Name = odd.Name,
                        Value = odd.Value.ToString(),
                        SpecialBetValue = odd.SpecialBetValue?.ToString(),
                        BetId = odd.BetId,
                    }).ToList()
                }).ToList()
            }).ToList();
        }

        public MatchViewModel MapMatchServiceModelToMatchViewModel(MatchServiceModel model)
        {
            return new MatchViewModel
            {
                ID = model.ID,
                Name = model.Name,
                StartDate = model.StartDate,
                EventId = model.EventId,
                MatchType = model.MatchType,
                Bets = model.Bets
                .Where(x => x.IsLive == true)
                .Select(bet => new BetViewModel
                {
                    ID = bet.ID,
                    Name = bet.Name,
                    IsLive = bet.IsLive,
                    MatchId = bet.MatchId,
                    Odds = bet.Odds.Select(odd => new OddViewModel
                    {
                        ID = odd.ID,
                        Name = odd.Name,
                        Value = odd.Value.ToString(),
                        SpecialBetValue = odd.SpecialBetValue?.ToString(),
                        BetId = odd.BetId,
                    }).ToList()
                }).ToList()
            };
        }
    }
}
