namespace IcoBet.Services
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    using IcoBet.Services.Models.XmlModels;

    public class ParsingService : IParsingService
    {
        
        public ParsingService()
        {
            
        }

        public XmlSports ParseXml(string xmlString)
        {
            try
            {
                var data = GenerateStreamFromString(xmlString);

                XmlSerializer serializer = new XmlSerializer(typeof(XmlSports));

                XmlSports sports = (XmlSports)serializer.Deserialize(data);

                return sports;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        private static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.Unicode.GetBytes(value ?? ""));
        }
    }
}
