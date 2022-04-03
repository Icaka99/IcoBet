namespace IcoBet.Services.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IcoBet.Data;
    using IcoBet.Data.Models;
    using IcoBet.Services.Data.Interfaces;
    using IcoBet.Services.Models.ServiceModels;
    using IcoBet.Services.Models.XmlModels;

    public class BetService : IBetService
    {
        private readonly ApplicationDbContext db;

        public BetService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(XmlBet input)
        {
            var dbBet = new Bet
            {
                ID = input.ID,
                Name = input.Name,
                IsLive = input.IsLive,
                MatchId = input.MatchId,
            };
            await this.db.Bets.AddAsync(dbBet);
            //await this.db.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(IEnumerable<XmlBet> input)
        {
            IEnumerable<Bet> dbBets = input.Select(x => new Bet
            {
                ID = x.ID,
                Name = x.Name,
                IsLive = x.IsLive,
                MatchId = x.MatchId,
            }).ToList();

            await this.db.Bets.AddRangeAsync(dbBets);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<BetServiceModel> GetBets()
        {
            var Bets = this.db.Bets
                .Select(x => new BetServiceModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsLive = x.IsLive,
                    MatchId = x.MatchId,
                }).ToList();

            return Bets.ToList();
        }

        public async Task UpdateAsync(XmlBet model)
        {
            var dbBet = this.db.Bets.FirstOrDefault(x => x.ID == model.ID);
            dbBet.Name = model.Name;
            dbBet.IsLive = model.IsLive;
            dbBet.MatchId = model.MatchId;

            //await this.db.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<XmlBet> model)
        {
            foreach (var bet in model)
            {
                var dbBet = this.db.Bets.FirstOrDefault(x => x.ID == bet.ID);
                dbBet.Name = bet.Name;
                dbBet.IsLive = bet.IsLive;
                dbBet.MatchId = bet.MatchId;
            }

            await this.db.SaveChangesAsync();

        }

        public async Task SaveRangeAsync(IEnumerable<XmlBet> model)
        {
            foreach (var bet in model)
            {
                if (this.db.Bets.Any(x => x.ID == bet.ID && x.MatchId == bet.MatchId))
                {
                    await this.UpdateAsync(bet);
                }
                else
                {
                    await this.CreateAsync(bet);
                }
            }

            //await this.db.SaveChangesAsync();
        }

        public BetServiceModel GetBet(string id)
        {
            var bet = this.db.Bets.Where(x => x.ID == id)
                .Select(x => new BetServiceModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsLive = x.IsLive,
                    MatchId = x.MatchId,
                })
                .FirstOrDefault();

            return bet;
        }

        public XmlBet GetBetDb(string id)
        {
            var bet = this.db.Bets.Where(x => x.ID == id)
                .Select(x => new XmlBet
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsLive = x.IsLive,
                    MatchId = x.MatchId,
                })
                .FirstOrDefault();

            return bet;
        }
    }
}
