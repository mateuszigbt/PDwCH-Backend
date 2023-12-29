using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quiz.Data;
using Quiz.DTO_s;
using Quiz.Model;
using System.Diagnostics;

namespace Quiz.Controllers
{
    [ApiController]
    [Route("api/quiz")]
    public class QuizController : Controller
    {
        private readonly QuizDbContext _quizDbContext;
        private readonly IMapper _mapper;
        public QuizController(IMapper mapper, QuizDbContext quizDbContext) 
        { 
            _quizDbContext = quizDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddQuiz(
        [FromForm] string title,
        [FromForm] string description,
        [FromForm] string category,
        [FromForm] int rating,
        [FromForm] List<string> answers,
        [FromForm] List<bool> correctAnswers,
        [FromForm] List<string> questions,
        [FromForm] List<IFormFile> images)
        {

            var existingCategory = _quizDbContext.Category.FirstOrDefault(c => c.NameCategory == category);

            if (existingCategory == null)
            {
                return NotFound("Bad chosen category");
            }

            var newQuiz = new Quiz.Model.Quiz
            {
                Title = title,
                Description = description,
                CategoryId = existingCategory.Id,
                Rating = rating,
                Answers = answers?.Select(answer => new Answer { AnswerText = answer }).ToList(),
                CorrectAnswers = correctAnswers?.Select(correctAnswer => new CorrectAnswer { correctAnswer = correctAnswer }).ToList(),
                Questions = questions?.Select(question => new Question { QuestionText = question }).ToList()
            };

            if (images != null && images.Any())
            {
                newQuiz.Images = new List<Image>();
                foreach (var imageFile in images)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        var image = new Image
                        {
                            ChoosenImage = memoryStream.ToArray(),
                            ContentType = imageFile.ContentType,
                        };
                        newQuiz.Images.Add(image);
                    }
                }
            }

            _quizDbContext.Quiz.Add(newQuiz);
            _quizDbContext.SaveChanges();

            return Ok(new { message = "Successfully added quiz" });
        }

        /*prawie
        [HttpPost]
        public IActionResult AddQuiz([FromForm] QuizDto quizDto)
        {
            var existingCategory = _quizDbContext.Category.FirstOrDefault(c => c.NameCategory == quizDto.Category.NameCategory);

            if (quizDto == null || existingCategory == null)
            {
                return BadRequest("Invalid data provided");
            }


            var newQuiz = new Quiz.Model.Quiz
            {
                Title = quizDto.Title,
                Description = quizDto.Description,
                CategoryId = existingCategory.Id,
                //===============Answers = quizDto.Answers.Add(answersDto => new Answer { AnswerText = answersDto.AnswerText }).ToList(),
                Answers = quizDto.Answers.Select(answerDto => new Answer { AnswerText = answerDto.AnswerText }).ToList(),
                CorrectAnswers = quizDto.CorrectAnswers.Select(correctAnswerDto => new CorrectAnswer { correctAnswer = correctAnswerDto.correctAnswer }).ToList(),
                Questions = quizDto.Questions.Select(questionDto => new Question { QuestionText = questionDto.QuestionText }).ToList()
            };

            if (quizDto.Images != null && quizDto.Images.Any())
            {
                newQuiz.Images = new List<Image>();
                foreach (var imageFile in quizDto.Images)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        var image = new Image
                        {
                            ChoosenImage = memoryStream.ToArray(),
                            ContentType = imageFile.ContentType
                        };
                        newQuiz.Images.Add(image);
                    }
                }
            }

            _quizDbContext.Quiz.Add(newQuiz);
            _quizDbContext.SaveChanges();
            return Ok(new { message = "Successfully added quiz" });
        }
        */

        [HttpGet]
        public IActionResult GetAllQuizes()
        {
            var allQuizzes = _quizDbContext.Quiz
                .Include(q => q.Category)
                .Include(q => q.Answers)
                .Include(q => q.CorrectAnswers)
                .Include(q => q.Questions)
                .Include(q => q.Images)
                .ProjectTo<QuizDto>(_mapper.ConfigurationProvider)
                //.AsSplitQuery()
                .ToList();

            if (allQuizzes == null || allQuizzes.Count == 0)
            {
                return NotFound("No quizzes found.");
            }

            return Ok(allQuizzes);
        }
    }
}
