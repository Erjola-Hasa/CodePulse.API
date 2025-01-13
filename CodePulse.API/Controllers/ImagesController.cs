using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase

    {
        private readonly IBlogImages _blogImages;
        public ImagesController(IBlogImages blogImages)
        {
            _blogImages = blogImages;

        }
        #region--------------- AddImages
        [HttpPost]
        public async Task<IActionResult> UploadImages([FromForm] IFormFile file, [FromForm] string fileName,
            [FromForm] string title)
        {
            ValidateFileUpload(file);
            if (ModelState.IsValid)
            {
                var blogimages = new BlogImages
                {
                    FileName = fileName,
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    Title = title,
                    
                    DataCreated = DateTime.Now,
                };

                blogimages = await _blogImages.UploadImages(file, blogimages);
                //convert domain to dto 

                var response = new BlogImagesDto
                {
                    Id = blogimages.Id,
                    Title = blogimages.Title,
                    FileExtension = blogimages.FileExtension,
                    FileName = blogimages.FileName,
                    Url = blogimages.Url,
                    DataCreated = DateTime.Now,
                };
                return Ok(response);

            }
            return BadRequest(ModelState);

        }


        private void ValidateFileUpload(IFormFile file)
        {
            var allowExtension = new string[] { ".jpg", "jpeg", "png" };

            if (!allowExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported File Format");

            }
            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }


        }
        #endregion-------------AddImages
        #region ---GetAllImages
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images =  await  _blogImages.GetAllImages();

            //Convert Domain Model to DTO 

            var Response = new List<BlogImagesDto>();
            foreach (var images1 in images)
            {
                Response.Add(new BlogImagesDto
                {
                    Id = images1.Id,
                    Title = images1.Title,
                    FileExtension = images1.FileExtension,
                    FileName = images1.FileName,
                    Url = images1.Url
                    
                });
            }
            return Ok(Response);

        }
        #endregion----GetAllImages

    }
}
