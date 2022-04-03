namespace IcoBet.Services
{
    using IcoBet.Services.Models.XmlModels;

    public interface IParsingService
    {
        XmlSports ParseXml(string xmlString);
    }
}
