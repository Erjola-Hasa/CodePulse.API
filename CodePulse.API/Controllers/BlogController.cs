using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]


    public class BlogController : ControllerBase
    {

        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogController(IBlogPostRepository blogPostRepository,ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }




        [HttpPost]
        public async Task<IActionResult> AddBlogPost( [FromBody] CreateRequestBlog blogpost)
        {

            //convert dto to domain

            var blogpostdomain = new BlogPost
            {

                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                UrlHandle = blogpost.UrlHandle,
                PublishedDate = blogpost.PublishedDate,
                Content = blogpost.Content,
                FeatureImageUrl = blogpost.FeatureImageUrl,
                Author = blogpost.Author,
                IsVisible = blogpost.IsVisible,
                Categories = new List<Category>()

            };
            foreach(var categoryId in blogpost.Categories)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
                if (existingCategory != null) 
                {
                    blogpostdomain.Categories.Add(existingCategory);
                }


            }
        blogpostdomain =  await _blogPostRepository.CreateAsync(blogpostdomain);

            //map domain to dto 
            var response = new BlogPostDto
            {
                Id=blogpostdomain.Id,
                Title = blogpostdomain.Title,
                ShortDescription = blogpostdomain.ShortDescription,
                UrlHandle = blogpostdomain.UrlHandle,
                PublishedDate = blogpostdomain.PublishedDate,
                Content = blogpostdomain.Content,
                FeatureImageUrl = blogpostdomain.FeatureImageUrl,
                Author = blogpostdomain.Author,
                IsVisible = blogpostdomain.IsVisible,
                Categories=blogpostdomain.Categories.Select(x=> new CategoryDto
                {
                    Id= x.Id,
                    Name= x.Name,
                    UrlHandle= x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult>GetAllPostBlog()
        {
            var blog = await _blogPostRepository.GetAllPost();

           var response = new List<BlogPostDto>();

            foreach (var blogpost in blog)
            {
                response.Add(new BlogPostDto
                {
                    Id= blogpost.Id,
                    Title=blogpost.Title,
                    ShortDescription=blogpost.ShortDescription,
                    UrlHandle = blogpost.UrlHandle,
                    PublishedDate = blogpost.PublishedDate,
                    Content = blogpost.Content,
                    FeatureImageUrl = blogpost.FeatureImageUrl,
                    Author = blogpost.Author,
                    IsVisible = blogpost.IsVisible,
                    Categories = blogpost.Categories.Select(x=> new CategoryDto 
                    { 
                        Id= x.Id,
                        Name= x.Name,
                        UrlHandle= x.UrlHandle, 
                        
                    }).ToList()

                });

            }
            return Ok(response);

        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult>UpdateBlogPost([FromRoute] Guid Id ,UpdateBlogPost updateBlogPost)
        {
            //Convert dto to domain
            var BlogPostDto = new BlogPost
            {
                Id = Id,
                Author = updateBlogPost.Author,
                ShortDescription = updateBlogPost.ShortDescription,
                UrlHandle = updateBlogPost.UrlHandle,
                PublishedDate = updateBlogPost.PublishedDate,
                Content = updateBlogPost.Content,
                FeatureImageUrl = updateBlogPost.FeatureImageUrl,
                IsVisible = updateBlogPost.IsVisible,
                Title = updateBlogPost.Title,
                Categories = new List<Category>()


            };
            foreach (var categoryId in updateBlogPost.Categories)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
                if(existingCategory != null)
                {
                    BlogPostDto.Categories.Add(existingCategory);
                }
            }
            
                BlogPostDto = await _blogPostRepository.UpdateAsync(BlogPostDto);

            if (BlogPostDto == null) 
            {
                return NotFound();
            }

            //Convert domain to dto
            var responseBlog = new BlogPostDto
            {

                Title = BlogPostDto.Title,
                Author = BlogPostDto.Author,
                ShortDescription = BlogPostDto.ShortDescription,
                UrlHandle = BlogPostDto.UrlHandle,
                IsVisible = BlogPostDto.IsVisible,
                FeatureImageUrl = BlogPostDto.FeatureImageUrl,
                Content = BlogPostDto.Content,
                PublishedDate = BlogPostDto.PublishedDate,
                Categories = BlogPostDto.Categories.Select(x=> new CategoryDto
                {
                    Id= x.Id,
                    Name = x.Name,
                    UrlHandle= x.UrlHandle,
                    
                }).ToList()

            };
           

            return Ok(BlogPostDto);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult>GetBlogById([FromRoute] Guid Id)
        {
            //convert dto to domain

            var response = await _blogPostRepository.GetByIdAsync(Id);
            if (response == null) 
            { 
                return NotFound();
            }
            //convert domain to dto 
            var existingResponse = new BlogPostDto
            {
                Id=Id,
                Author = response.Author,
                ShortDescription = response.ShortDescription,
                UrlHandle = response.UrlHandle,
                Title = response.Title,
                FeatureImageUrl = response.FeatureImageUrl,
                Content = response.Content,
                PublishedDate = response.PublishedDate,
                IsVisible = response.IsVisible,
            };
            
            return Ok(existingResponse);



        }


        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid Id)
        {
            var response = await _blogPostRepository.DeleteAsync(Id);

            if(response == null)
            {
                return NotFound();
            }
            var responseblog = new BlogPostDto
            {
                Id=response.Id,
                Author = response.Author,
                Title = response.Title,
                UrlHandle = response.UrlHandle,
                FeatureImageUrl = response.FeatureImageUrl,
                Content = response.Content,
                PublishedDate = response.PublishedDate,
                IsVisible = response.IsVisible,
                ShortDescription = response.ShortDescription

            };
            return Ok(responseblog);
        }

    }
}
