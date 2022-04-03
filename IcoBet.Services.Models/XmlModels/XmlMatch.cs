namespace IcoBet.Services.Models.XmlModels
{
    using System;
    using System.Xml.Serialization;

    [Serializable()]
    public class XmlMatch
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute("MatchType")]
        public string MatchType { get; set; }

        public string EventId { get; set; }

        [XmlElement("Bet")]
        public XmlBet[] Bets { get; set; }
    }
}
