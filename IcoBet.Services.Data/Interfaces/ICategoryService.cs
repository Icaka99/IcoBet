namespace IcoBet.Services.Data.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using IcoBet.Data.Models;

    public interface ICategoryService
    {
        Task CreateAsync(Category input);

        Task UpdateAsync(Category model);

        IEnumerable<Category> GetCategories();

        Category GetCategory(string id);
    }
}
