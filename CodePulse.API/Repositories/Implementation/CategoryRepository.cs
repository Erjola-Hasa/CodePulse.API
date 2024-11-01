using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        //implementation of interface
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

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory= await _dbcontext.categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCategory==null)
            {
                return null;
            }
               _dbcontext.categories.Remove(existingCategory);
            await _dbcontext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
           return await _dbcontext.categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
          return  await _dbcontext.categories.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var existingcategories = await _dbcontext.categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (existingcategories != null)

            { 
                _dbcontext.Entry(existingcategories).CurrentValues.SetValues(category);
                await _dbcontext.SaveChangesAsync();
                return category;
            }
           
            return null;

        }
    }
}
