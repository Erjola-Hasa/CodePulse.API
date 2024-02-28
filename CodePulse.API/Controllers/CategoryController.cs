using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository )
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost]
       
        public async Task<IActionResult> CreateCategory( CategoryRequestDto request)
        {
           
            ////map DTO to DomainModel
            var category = new Category()
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,

            };
             await _categoryRepository.CreateAsync(category);

            //await _context.categories.AddAsync(category);
            //await _context.SaveChangesAsync();

            /////DomainModel to DTO 

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,

            };
             return Ok(response);

        }
    }
}
