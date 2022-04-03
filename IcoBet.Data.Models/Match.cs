namespace IcoBet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Match
    {
        public Match()
        {
            this.Bets = new HashSet<Bet>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public MatchTypeEnum MatchType { get; set; }

        public string EventId { get; set; }

        public virtual Event Event { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
