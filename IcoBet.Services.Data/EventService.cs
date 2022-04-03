namespace IcoBet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using IcoBet.Data;
    using IcoBet.Data.Models;
    using IcoBet.Services.Data.Interfaces;
    using IcoBet.Services.Models.XmlModels;

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext db;

        public EventService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(XmlEvent input)
        {
            var dbEvent = new Event
            {
                ID = input.ID,
                Name = input.Name,
                IsLive = input.IsLive,
                CategoryId = input.CategoryId
            };
            await this.db.Events.AddAsync(dbEvent);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<XmlEvent> GetEvents()
        {
            var Events = this.db.Events
                .Select(x => new XmlEvent
                {
                    ID = x.ID,
                    Name = x.Name,
                    CategoryId = x.CategoryId,
                    IsLive = x.IsLive
                }).ToList();

            return Events.ToList();
        }

        public async Task UpdateAsync(XmlEvent model)
        {
            var dbEvent = this.db.Events.FirstOrDefault(x => x.ID == model.ID);
            dbEvent.Name = model.Name;
            dbEvent.CategoryId = model.CategoryId;
            dbEvent.IsLive = model.IsLive;

            await this.db.SaveChangesAsync();
        }

        public XmlEvent GetEvent(string id)
        {
            var eventDb = this.db.Events.Where(x => x.ID == id)
                .Select(x => new XmlEvent
                {
                    ID = x.ID,
                    Name = x.Name,
                    CategoryId = x.CategoryId,
                    IsLive = x.IsLive,
                })
                .FirstOrDefault();

            return eventDb;
        }
    }
}
