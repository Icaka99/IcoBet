namespace IcoBet.Services.Models.XmlModels
{
    using System;
    using System.Xml.Serialization;

    [Serializable()]
    public class XmlSport
    {
        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Event")]
        public XmlEvent[] Events { get; set; }
    }
}
