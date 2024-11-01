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
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost]

        public async Task<IActionResult> CreateCategory(CategoryRequestDto request)
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
        [HttpGet]

        public async Task<IActionResult> GetallCategory()
        {
            var categories = await _categoryRepository.GetAllAsync();
            //map domain model to dto 
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });

            }
            return Ok(response);



        }

        //getById
        [HttpGet]
        [Route("{id:Guid}")]

        //from route dmth qe vjen nga url 
        public async Task<IActionResult>GetCategoriesById([FromRoute]Guid id)
        {
            var existingCategories= await _categoryRepository.GetByIdAsync(id);

            if(existingCategories == null)
            {
               return NotFound();

            }
            var response = new CategoryDto
            {
                Id = existingCategories.Id,
                Name = existingCategories.Name,
                UrlHandle = existingCategories.UrlHandle,
            };
            return Ok(response);

        }
        //update

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult>UpdateCategory([FromRoute]Guid id,UpdateCategoryRequest updateCategoryRequest)
        {
            //Convert DTO To Domain Model
            var category = new Category
            {

                Id = id,
                Name = updateCategoryRequest.Name,
                UrlHandle = updateCategoryRequest.UrlHandle,
            };

            category= await _categoryRepository.UpdateCategoryAsync(category);

            if (category == null)
            {
                {
                    return NotFound();
                }
            }
            //Convert DOMAIN  model to DTO
            var response = new CategoryDto
            {
                Id= category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }



        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult>DeleteCategory([FromRoute]Guid id)
        {
            var category = await _categoryRepository.DeleteAsync(id);

            if (category is null)
            {

                return NotFound();
                
            }
            var response = new CategoryDto
            { 
                Id =category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(response);
        }
    }
}
