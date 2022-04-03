namespace IcoBet.Services.Models.ServiceModels
{
    using System;
    using System.Collections.Generic;

    public class MatchServiceModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        public string EventId { get; set; }

        public IEnumerable<BetServiceModel> Bets { get; set; }
    }
}
