namespace IcoBet.Services.Data.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using IcoBet.Services.Models.XmlModels;
    using IcoBet.Services.Models.ServiceModels;

    public interface IBetService
    {
        Task CreateAsync(XmlBet input);

        Task CreateRangeAsync(IEnumerable<XmlBet> input);

        Task UpdateAsync(XmlBet model);

        Task UpdateRangeAsync(IEnumerable<XmlBet> model);

        Task SaveRangeAsync(IEnumerable<XmlBet> model);

        IEnumerable<BetServiceModel> GetBets();

        BetServiceModel GetBet(string id);

        XmlBet GetBetDb(string id);
    }
}
