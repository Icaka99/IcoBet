namespace IcoBet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Bet
    {
        public Bet()
        {
            this.Odds = new HashSet<Odd>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public string MatchId { get; set; }

        public virtual Match Match { get; set; }

        public virtual ICollection<Odd> Odds { get; set; }
    }
}
