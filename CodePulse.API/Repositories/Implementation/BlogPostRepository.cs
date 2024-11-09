using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
   
    public class BlogPostRepository:IBlogPostRepository
    {
        private readonly AplicationDbContext _dbcontext;
        public BlogPostRepository(AplicationDbContext dbContext)
        {
          _dbcontext = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _dbcontext.blogPosts.AddAsync(blogPost);
            await _dbcontext.SaveChangesAsync();
            return blogPost;
        }

     

        public async Task<IEnumerable<BlogPost>> GetAllPost()
        {

            return await _dbcontext.blogPosts.ToListAsync();
        }

        public Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }

}
