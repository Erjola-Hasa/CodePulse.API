﻿using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        //definition of method
        Task<Category>CreateAsync(Category category);

        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(Guid id);

       
        Task<Category?> UpdateCategoryAsync(Category category);

        Task <Category?> DeleteAsync(Guid id);

    }
}
