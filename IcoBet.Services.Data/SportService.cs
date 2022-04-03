namespace IcoBet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using IcoBet.Data;
    using IcoBet.Data.Models;
    using IcoBet.Services.Data.Interfaces;
    using IcoBet.Services.Models.XmlModels;
    

    public class SportService : ISportService
    {
        private readonly ApplicationDbContext db;

        public SportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(XmlSport input)
        {
            var dbSport = new Sport
            {
                ID = input.ID,
                Name = input.Name,
            };
            await this.db.Sports.AddAsync(dbSport);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<XmlSport> GetSports()
        {
            var sports = this.db.Sports
                .Select(x => new XmlSport
                {
                    ID = x.ID,
                    Name = x.Name
                }).ToList();

            return sports.ToList();
        }

        public async Task UpdateAsync(XmlSport model)
        {
            var sport = this.db.Sports.FirstOrDefault(x => x.ID == model.ID);
            sport.Name = model.Name;

            await this.db.SaveChangesAsync();
        }

        public XmlSport GetSport(string id)
        {
            var sport = this.db.Sports.Where(x => x.ID == id)
                .Select(x => new XmlSport
                {
                    ID = x.ID,
                    Name = x.Name,
                })
                .FirstOrDefault();

            return sport;
        }
    }
}
