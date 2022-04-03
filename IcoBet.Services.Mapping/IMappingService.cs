namespace IcoBet.Services.Mapping
{
    using System.Collections.Generic;

    using IcoBet.Web.ViewModels.Match;
    using IcoBet.Services.Models.ServiceModels;

    public interface IMappingService
    {
        IEnumerable<MatchViewModel> MapMatchServiceModelsToMatchViewModels(IEnumerable<MatchServiceModel> model);

        MatchViewModel MapMatchServiceModelToMatchViewModel(MatchServiceModel model);
    }
}
