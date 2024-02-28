using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
       private readonly AplicationDbContext _dbcontext;
        public CategoryRepository(AplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
                
        }
        public async Task<Category> CreateAsync(Category category)
        {
           await _dbcontext.categories.AddAsync(category);
            await _dbcontext.SaveChangesAsync();
            return category;
        }
    }
}
