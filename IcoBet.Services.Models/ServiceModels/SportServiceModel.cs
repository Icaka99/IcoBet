namespace IcoBet.Services.Models.ServiceModels
{
    using System.Collections.Generic;

    public class SportServiceModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public ICollection<EventServiceModel> Events { get; set; }
    }
}
