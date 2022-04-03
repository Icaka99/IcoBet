namespace IcoBet.Services.Models.ServiceModels
{
    using System.Collections.Generic;

    public class BetServiceModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public string MatchId { get; set; }

        public IEnumerable<OddServiceModel> Odds { get; set; }
    }
}
