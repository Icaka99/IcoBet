namespace IcoBet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using IcoBet.Data;
    using IcoBet.Data.Models;
    using IcoBet.Services.Data.Interfaces;
    using IcoBet.Services.Models.XmlModels;
    using IcoBet.Services.Models.ServiceModels;

    public class OddService : IOddService
    {
        private readonly ApplicationDbContext db;

        public OddService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(XmlOdd input)
        {
            var specialBetValue =  input.SpecialBetValue == null ? null : (double?)double.Parse(input.SpecialBetValue.Replace('.', ',').Replace(':', ','));
            var value = double.Parse(input.Value.Replace('.', ','));
            var dbOdd = new Odd
            {
                ID = input.ID,
                Name = input.Name,
                SpecialBetValue = specialBetValue,
                Value = value,
                BetId = input.BetId
            };
            await this.db.Odds.AddAsync(dbOdd);
            //await this.db.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(IEnumerable<XmlOdd> input)
        {
            IEnumerable<Odd> dbOdds = input.Select(x => new Odd
            {
                ID = x.ID,
                Name = x.Name,
                SpecialBetValue = x.SpecialBetValue == null ? null : (double?)double.Parse(x.SpecialBetValue.Replace('.', ',').Replace(':', ',')),
                Value = double.Parse(x.Value.Replace('.', ',')),
                BetId = x.BetId,
            }).ToList();

            await this.db.Odds.AddRangeAsync(dbOdds);
            await this.db.SaveChangesAsync();
        }

        public async Task SaveRangeAsync(IEnumerable<XmlOdd> model, string betId)
        {
            foreach (var odd in model)
            {
                if (this.db.Odds.Any(x => x.ID == odd.ID && x.BetId == betId))
                {
                    await this.UpdateAsync(odd);
                }
                else
                {
                    await this.CreateAsync(odd);
                }
            }

            await this.db.SaveChangesAsync();
        }

        public IEnumerable<OddServiceModel> GetOdds()
        {
            var Odds = this.db.Odds
                .Select(x => new OddServiceModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    SpecialBetValue = x.SpecialBetValue.ToString(),
                    Value = x.Value.ToString(),
                    BetId = x.BetId,
                }).ToList();

            return Odds.ToList();
        }

        public async Task UpdateAsync(XmlOdd model)
        {
            var specialBetValue = model.SpecialBetValue == null ? null : (double?)double.Parse(model.SpecialBetValue.Replace('.', ',').Replace(':', ','));
            var value = double.Parse(model.Value.Replace('.', ','));

            var dbOdd = this.db.Odds.FirstOrDefault(x => x.ID == model.ID);
            dbOdd.Name = model.Name;
            dbOdd.SpecialBetValue = specialBetValue;
            dbOdd.Value = value;

            //await this.db.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<XmlOdd> model)
        {
            foreach (var odd in model)
            {
                var dbOdd = this.db.Odds.FirstOrDefault(x => x.ID == odd.ID);
                dbOdd.Name = odd.Name;
                dbOdd.SpecialBetValue = odd.SpecialBetValue == null ? null : (double?)double.Parse(odd.SpecialBetValue.Replace('.', ',').Replace(':', ','));
                dbOdd.Value = double.Parse(odd.Value.Replace('.', ','));
                dbOdd.BetId = odd.BetId;
            }

            await this.db.SaveChangesAsync();

            //IEnumerable<Odd> dbOdds = model.Select(x => new Odd
            //{
            //    ID = x.ID,
            //    Name = x.Name,
            //    SpecialBetValue = x.SpecialBetValue == null ? null : (double?)double.Parse(x.SpecialBetValue.Replace('.', ',').Replace(':', ',')),
            //    Value = double.Parse(x.Value.Replace('.', ',')),
            //    BetId = x.BetId
            //}).ToList();


        }

        public OddServiceModel GetOdd(string id)
        {
            var odd = this.db.Odds.Where(x => x.ID == id)
                .Select(x => new OddServiceModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Value = x.Value.ToString(),
                    SpecialBetValue = x.SpecialBetValue.ToString(),
                    BetId = x.BetId,
                })
                .FirstOrDefault();

            return odd;
        }
    }
}
