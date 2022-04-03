namespace IcoBet.Services.Data.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using IcoBet.Services.Models.XmlModels;
    
    public interface IEventService
    {
        Task CreateAsync(XmlEvent input);

        Task UpdateAsync(XmlEvent model);

        IEnumerable<XmlEvent> GetEvents();

        XmlEvent GetEvent(string id);
    }
}
