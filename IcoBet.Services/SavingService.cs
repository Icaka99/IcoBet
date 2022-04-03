namespace IcoBet.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using IcoBet.Data;
    using IcoBet.Data.Models;
    using IcoBet.Common;
    using IcoBet.Services.Data.Interfaces;
    using IcoBet.Services.Models.XmlModels;

    public class SavingService : ISavingService
    {
        private readonly ApplicationDbContext db;
        private readonly ISportService sportService;
        private readonly IEventService eventService;
        private readonly ICategoryService categoryService;
        private readonly IMatchService matchService;
        private readonly IBetService betService;
        private readonly IOddService oddService;

        public SavingService(ApplicationDbContext db,
            ISportService sportService,
            IEventService eventService,
            ICategoryService categoryService,
            IMatchService matchService,
            IBetService betService,
            IOddService oddService)
        {
            this.db = db;
            this.sportService = sportService;
            this.eventService = eventService;
            this.categoryService = categoryService;
            this.matchService = matchService;
            this.betService = betService;
            this.oddService = oddService;
        }

        public async Task SaveToDb(XmlSports sports)
        {

            foreach (var xmlSport in sports.Sports)
            {
                var dbSport = this.sportService.GetSport(xmlSport.ID);

                if (dbSport != null)
                {
                    await this.sportService.UpdateAsync(xmlSport);
                }
                else
                {
                    await this.sportService.CreateAsync(xmlSport);
                }

                foreach (var xmlEvent in xmlSport.Events)
                {
                    var dbEvent = this.eventService.GetEvent(xmlEvent.ID);
                    var dbCategory = this.categoryService.GetCategory(xmlEvent.CategoryId);

                    if (dbCategory == null)
                    {
                        var categoryToAdd = new Category
                        {
                            ID = xmlEvent.CategoryId,
                            Name = GlobalConstants.DefaultCategoryName,
                        };

                        await this.categoryService.CreateAsync(categoryToAdd);
                    }

                    if (dbEvent != null)
                    {
                        await this.eventService.UpdateAsync(xmlEvent);
                    }
                    else
                    {
                        await this.eventService.CreateAsync(xmlEvent);
                    }

                    HashSet<XmlBet> betsToUpdate = new HashSet<XmlBet>();
                    HashSet<XmlBet> betsToCreate = new HashSet<XmlBet>();

                    HashSet<XmlOdd> oddsToUpdate = new HashSet<XmlOdd>();
                    HashSet<XmlOdd> oddsToCreate = new HashSet<XmlOdd>();

                    foreach (var xmlMatch in xmlEvent.Matches)
                    {
                        if (xmlMatch.MatchType == "Outright")
                        {
                            continue;
                        }

                        var dbMatch = this.matchService.GetMatchDb(xmlMatch.ID);

                        if (dbMatch != null)
                        {
                            xmlMatch.EventId = xmlEvent.ID;
                            await this.matchService.UpdateAsync(xmlMatch);
                        }
                        else
                        {
                            xmlMatch.EventId = xmlEvent.ID;
                            await this.matchService.CreateAsync(xmlMatch);
                        }

                        if (xmlMatch.Bets != null)
                        {

                            foreach (var xmlBet in xmlMatch.Bets)
                            {
                                xmlBet.MatchId = xmlMatch.ID;

                                if (this.db.Bets.Any(x => x.ID == xmlBet.ID && x.MatchId == xmlMatch.ID))
                                {
                                    betsToUpdate.Add(xmlBet);
                                }
                                else
                                {
                                    betsToCreate.Add(xmlBet);
                                }

                                if (xmlBet.Odds != null)
                                {
                                    foreach (var xmlOdd in xmlBet.Odds)
                                    {
                                        xmlOdd.BetId = xmlBet.ID;

                                        if (this.db.Odds.Any(x => x.ID == xmlOdd.ID && xmlOdd.BetId == xmlBet.ID))
                                        {
                                            oddsToUpdate.Add(xmlOdd);
                                        }
                                        else
                                        {
                                            oddsToCreate.Add(xmlOdd);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    await this.betService.CreateRangeAsync(betsToCreate);
                    await this.betService.UpdateRangeAsync(betsToUpdate);

                    await this.oddService.CreateRangeAsync(oddsToCreate);
                    await this.oddService.UpdateRangeAsync(oddsToUpdate);
                }
            }

            await this.db.SaveChangesAsync();
        }
    }
}
