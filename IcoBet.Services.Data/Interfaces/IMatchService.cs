namespace IcoBet.Services.Data.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using IcoBet.Services.Models.XmlModels;
    using IcoBet.Services.Models.ServiceModels;

    public interface IMatchService
    {
        Task CreateAsync(XmlMatch input);

        Task UpdateAsync(XmlMatch model);

        IEnumerable<MatchServiceModel> GetMatchesForTheNextTwentyFourHours();

        MatchServiceModel GetMatch(string id);

        XmlMatch GetMatchDb(string id);
    }
}
