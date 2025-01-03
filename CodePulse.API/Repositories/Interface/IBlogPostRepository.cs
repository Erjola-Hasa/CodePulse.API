﻿using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
         Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>>GetAllPost();
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?> GetByIdAsync(Guid id);

        Task<BlogPost?> DeleteAsync(Guid Id);
    }
}
