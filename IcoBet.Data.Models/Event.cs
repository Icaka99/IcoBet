namespace IcoBet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        public Event()
        {
            this.Matches = new HashSet<Match>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
