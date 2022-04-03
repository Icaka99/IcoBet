namespace IcoBet.Services.Models.ServiceModels
{
    using System.Collections.Generic;

    public class EventServiceModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public bool IsLive { get; set; }

        public ICollection<MatchServiceModel> Matches { get; set; }
    }
}
