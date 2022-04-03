namespace IcoBet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using IcoBet.Data;
    using IcoBet.Data.Models;
    using IcoBet.Services.Data.Interfaces;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext db;

        public CategoryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(Category input)
        {
            var dbCategory = new Category
            {
                ID = input.ID,
                Name = input.Name,
            };
            await this.db.Categories.AddAsync(dbCategory);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<Category> GetCategories()
        {
            var Categorys = this.db.Categories
                .Select(x => new Category
                {
                    ID = x.ID,
                    Name = x.Name,
                }).ToList();

            return Categorys.ToList();
        }

        public async Task UpdateAsync(Category model)
        {
            var dbCategory = this.db.Categories.FirstOrDefault(x => x.ID == model.ID);
            dbCategory.Name = model.Name;

            await this.db.SaveChangesAsync();
        }

        public Category GetCategory(string id)
        {
            var category = this.db.Categories.Where(x => x.ID == id)
                .Select(x => new Category
                {
                    ID = x.ID,
                    Name = x.Name,
                })
                .FirstOrDefault();

            return category;
        }
    }
}
