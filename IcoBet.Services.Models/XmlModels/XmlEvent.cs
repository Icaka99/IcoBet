namespace IcoBet.Services.Models.XmlModels
{
    using System;
    using System.Xml.Serialization;

    [Serializable()]
    public class XmlEvent
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("CategoryID")]
        public string CategoryId { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlElement("Match")]
        public XmlMatch[] Matches { get; set; }
    }
}
