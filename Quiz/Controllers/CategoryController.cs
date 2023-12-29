using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Quiz.Data;
using Quiz.DTO_s.Profiles;
using Quiz.Model;

namespace Quiz.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly QuizDbContext _quizDbContext;
        private readonly IMapper _mapper;

        public CategoryController(QuizDbContext quizDbContext, IMapper mapper)
        {
            _quizDbContext = quizDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var categories = _quizDbContext.Category.ToList();

            if (categories == null || categories.Count == 0)
            {
                return NotFound("Not found categories");
            }

            List<CategoryDto> allCategories = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(allCategories);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryDto categoryDto) 
        {
            var category = _mapper.Map<Category>(categoryDto);
            _quizDbContext.Category.Add(category);
            _quizDbContext.SaveChanges();
            return Ok("Succesfully added user");
        }
    }
}
