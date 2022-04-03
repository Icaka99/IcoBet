namespace IcoBet.Services.Models.XmlModels
{
    using System;
    using System.Xml.Serialization;

    [Serializable()]
    [XmlRoot("XmlSports")]
    public class XmlSports
    {
        [XmlElement("Sport")]
        public XmlSport[] Sports { get; set; }
    }
}
