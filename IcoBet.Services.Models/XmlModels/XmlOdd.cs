namespace IcoBet.Services.Models.XmlModels
{
    using System;
    using System.Xml.Serialization;

    [Serializable()]
    public class XmlOdd
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }

        [XmlAttribute("SpecialBetValue")]
        public string SpecialBetValue { get; set; }

        public string BetId { get; set; }
    }
}
