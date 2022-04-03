namespace IcoBet.Services
{
    using System.Threading.Tasks;

    using IcoBet.Services.Models.XmlModels;

    public interface ISavingService
    {
        Task SaveToDb(XmlSports sports);
    }
}
