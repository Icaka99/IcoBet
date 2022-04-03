namespace IcoBet.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Odd
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public double? SpecialBetValue { get; set; }

        public string BetId { get; set; }

        public virtual Bet Bet { get; set; }
    }
}
