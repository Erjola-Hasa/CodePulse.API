using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogImages
    {
       Task<BlogImages>  UploadImages(IFormFile file,BlogImages blogImages);
      public Task<IEnumerable<BlogImages>>GetAllImages();
    }
}
