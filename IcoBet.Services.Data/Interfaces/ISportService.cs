namespace IcoBet.Services.Data.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using IcoBet.Services.Models.XmlModels;

    public interface ISportService
    {
        Task CreateAsync(XmlSport input);

        Task UpdateAsync(XmlSport model);

        IEnumerable<XmlSport> GetSports();

        XmlSport GetSport(string id);
    }
}
