namespace IcoBet.Web.ViewModels.Bet
{
    using System.Collections.Generic;

    using IcoBet.Web.ViewModels.Odd;

    public class BetViewModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public string MatchId { get; set; }

        public IEnumerable<OddViewModel> Odds { get; set; }
    }
}
