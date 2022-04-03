namespace IcoBet.Services.Data.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using IcoBet.Services.Models.XmlModels;
    using IcoBet.Services.Models.ServiceModels;

    public interface IOddService
    {
        Task CreateAsync(XmlOdd input);

        Task CreateRangeAsync(IEnumerable<XmlOdd> input);

        Task UpdateAsync(XmlOdd model);

        Task UpdateRangeAsync(IEnumerable<XmlOdd> model);

        Task SaveRangeAsync(IEnumerable<XmlOdd> model, string betId);

        IEnumerable<OddServiceModel> GetOdds();

        OddServiceModel GetOdd(string id);
    }
}
