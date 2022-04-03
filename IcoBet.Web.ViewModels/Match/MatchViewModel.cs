namespace IcoBet.Web.ViewModels.Match
{
    using System;
    using System.Collections.Generic;

    using IcoBet.Web.ViewModels.Bet;

    public class MatchViewModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        public string EventId { get; set; }

        public IEnumerable<BetViewModel> Bets { get; set; }
    }
}
