using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class ImagesRepository : IBlogImages
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AplicationDbContext _context;
        public ImagesRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor
            ,AplicationDbContext aplicationDbContext)
        {
            _environment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _context = aplicationDbContext;
            
        }

        public async Task<IEnumerable<BlogImages>> GetAllImages()
        {
            return await _context.BlogImages.ToListAsync();
        }

        public async Task<BlogImages> UploadImages(IFormFile file, BlogImages blogImages)
        {
            //upload the images to API/Images
            var localpath = Path.Combine(_environment.ContentRootPath, "Images", $"{blogImages.FileName}{blogImages.FileExtension}");
            using var stream = new FileStream(localpath,FileMode.Create);
            await file.CopyToAsync(stream);

            //Update the database 
            var httprequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httprequest.Scheme}://{httprequest.Host}{httprequest.PathBase}/Images/{blogImages.FileName}{blogImages.FileExtension}";
            blogImages.Url = urlPath;

            await _context.BlogImages.AddAsync(blogImages);
            await _context.SaveChangesAsync();
            return blogImages;

        }
    }
}
