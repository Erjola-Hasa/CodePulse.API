using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace CodePulse.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]


    public class BlogController : ControllerBase
    {

        private readonly IBlogPostRepository _blogPostRepository;

        public BlogController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
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
                IsVisible = blogpost.IsVisible


            };
        blogpostdomain =  await _blogPostRepository.CreateAsync(blogpostdomain);

            //map domain to dto 
            var response = new BlogPostDto
            {
                Title = blogpostdomain.Title,
                ShortDescription = blogpostdomain.ShortDescription,
                UrlHandle = blogpostdomain.UrlHandle,
                PublishedDate = blogpostdomain.PublishedDate,
                Content = blogpostdomain.Content,
                FeatureImageUrl = blogpostdomain.FeatureImageUrl,
                Author = blogpostdomain.Author,
                IsVisible = blogpostdomain.IsVisible
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
                    IsVisible = blogpost.IsVisible

                });

            }
            return Ok(response);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult>UpdateBlogPost( [FromRoute] Guid id ,UpdateBlogPost updateBlogPost)
        {
            //Convert dto to domain
            var BlogPostDto = new BlogPost
            {
                Author = updateBlogPost.Author,
                ShortDescription = updateBlogPost.ShortDescription,
                UrlHandle = updateBlogPost.UrlHandle,
                PublishedDate = updateBlogPost.PublishedDate,
                Content = updateBlogPost.Content,
                FeatureImageUrl = updateBlogPost.FeatureImageUrl,
                IsVisible = updateBlogPost.IsVisible,
                Title = updateBlogPost.Title,


            };
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

            };

            return Ok(BlogPostDto);
        }

    }
}
