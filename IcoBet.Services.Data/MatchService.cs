namespace IcoBet.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using IcoBet.Data;
    using IcoBet.Data.Models;
    using IcoBet.Services.Data.Interfaces;
    using IcoBet.Services.Models.XmlModels;
    using IcoBet.Services.Models.ServiceModels;

    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext db;

        public MatchService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(XmlMatch input)
        {
            var dbMatch = new Match
            {
                ID = input.ID,
                Name = input.Name,
                StartDate = input.StartDate,
                MatchType = ParseStringToEnum(input.MatchType),
                EventId = input.EventId,
            };
            await this.db.Matches.AddAsync(dbMatch);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<MatchServiceModel> GetMatchesForTheNextTwentyFourHours()
        {
            var matches = this.db.Matches
                .Where(x => x.StartDate <= DateTime.Now.AddHours(24) && x.StartDate > DateTime.Now)
                .Select(x => new MatchServiceModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    MatchType = x.MatchType.ToString(),
                    EventId = x.EventId,
                    Bets = x.Bets
                    .Where(x => x.IsLive == true && (x.Name == "Match Winner" || x.Name == "Map Advantage" || x.Name == "Total Maps Played"))
                    .Select(bet => new BetServiceModel
                    {
                        ID = bet.ID,
                        Name = bet.Name,
                        IsLive = bet.IsLive,
                        MatchId = bet.MatchId,
                        Odds = bet.Odds
                        .Where(x => x.SpecialBetValue != null)
                        .Select(odd => new OddServiceModel
                        {
                            ID = odd.ID,
                            Name = odd.Name,
                            Value = odd.Value.ToString(),
                            SpecialBetValue = odd.SpecialBetValue.ToString(),
                            BetId = odd.BetId,
                        }).ToList()
                    }).ToList()
                }).ToList();

            return matches.ToList();
        }

        public async Task UpdateAsync(XmlMatch model)
        {
            var dbMatch = this.db.Matches.FirstOrDefault(x => x.ID == model.ID);
            dbMatch.Name = model.Name;
            dbMatch.StartDate = model.StartDate;
            dbMatch.MatchType = ParseStringToEnum(model.MatchType);
            dbMatch.EventId = model.EventId;

            await this.db.SaveChangesAsync();
        }

        public MatchServiceModel GetMatch(string id)
        {
            var bets = this.db.Bets.Where(x => x.Match.ID == id).ToList();

            var match = this.db.Matches.Where(x => x.ID == id)
                .Select(x => new MatchServiceModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    MatchType = ParseEnum(x.MatchType),
                    EventId = x.EventId,
                    Bets = x.Bets.Select(bet => new BetServiceModel
                    {
                        ID = bet.ID,
                        Name = bet.Name,
                        IsLive = bet.IsLive,
                        MatchId = bet.MatchId,
                        Odds = bet.Odds.Select(odd => new OddServiceModel
                        {
                            ID = odd.ID,
                            Name = odd.Name,
                            Value = odd.Value.ToString(),
                            SpecialBetValue = odd.SpecialBetValue.ToString(),
                            BetId = odd.BetId,
                        }).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return match;
        }

        public XmlMatch GetMatchDb(string id)
        {
            var match = this.db.Matches.Where(x => x.ID == id)
                .Select(x => new XmlMatch
                {
                    ID = x.ID,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    MatchType = ParseEnum(x.MatchType),
                    EventId = x.EventId,
                })
                .FirstOrDefault();

            return match;
        }

        private static string ParseEnum(MatchTypeEnum enumString)
        {
            if (enumString == MatchTypeEnum.Prematch)
            {
                return "PreMatch";
            }
            else if (enumString == MatchTypeEnum.Live)
            {
                return "Live";
            }
            else
            {
                return "Outright";
            }
        }

        private static MatchTypeEnum ParseStringToEnum(string enumString)
        {
            if (enumString == "PreMatch")
            {
                return MatchTypeEnum.Prematch;
            }
            else if (enumString == "Live")
            {
                return MatchTypeEnum.Live;
            }
            else
            {
                return MatchTypeEnum.Outright;
            }
        }
    }
}
