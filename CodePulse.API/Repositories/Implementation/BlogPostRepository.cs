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

        public async Task<BlogPost?> DeleteAsync(Guid Id)
        {
           var deleteBlog= await _dbcontext.blogPosts.FirstOrDefaultAsync(x => x.Id == Id);
            if (deleteBlog == null)
            {
                return null;
            }
            _dbcontext.blogPosts.Remove(deleteBlog);
            await _dbcontext.SaveChangesAsync();
            return deleteBlog;
                

        }

        public async Task<IEnumerable<BlogPost>> GetAllPost()
        {

            return await _dbcontext.blogPosts.ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
           return await _dbcontext.blogPosts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingblogpost = await _dbcontext.blogPosts.FirstOrDefaultAsync(x=>x.Id == blogPost.Id);
            if (existingblogpost != null)
            {
                _dbcontext.Entry(existingblogpost).CurrentValues.SetValues(blogPost);
               await _dbcontext.SaveChangesAsync(true);
                return blogPost;

            }
            return null;
        }
    }

}
