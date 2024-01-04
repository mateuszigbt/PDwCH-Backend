using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.DTO_s;
using Quiz.Model;

namespace Quiz.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly QuizDbContext _quizDbContexts;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, QuizDbContext quizDbContext)
        {
            _mapper = mapper;
            _quizDbContexts = quizDbContext;
        }
        /*
        [HttpGet("{id}")]
        public IActionResult GetUser(int id) 
        {
            var user = _quizDbContexts.Users
                .Include(q => q.Points)
                .Include(q => q.QuizProfile)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
        */
        [HttpPost]
        public IActionResult AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var newQuiz = new Quiz.Model.User
            {
                Login = userDto.Login,
                Password = userDto.Password,
                Email = userDto.Email,
                IsAdmin = userDto.IsAdmin,
                QuizProfile = userDto.QuizProfile?.Select(quizProfile => new QuizProfile
                {
                    Category = quizProfile.Category,
                    Rating = quizProfile.Rating,
                    Title = quizProfile.Title,
                    Points = quizProfile.Points?.Select(points => new Points
                    {
                        Score = points.Score
                    }).ToList()
                }).ToList()
            };

            //var user = _mapper.Map<User>(userDto);
            _quizDbContexts.Users.Add(newQuiz);
            _quizDbContexts.SaveChanges();
            return Ok(new {message = "Succesfully added user"});
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var allUsers = _quizDbContexts.Users
                .Include(u => u.QuizProfile)
                .ThenInclude(qu => qu.Points)
                .ToList();

            if (allUsers == null || allUsers.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(allUsers);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _quizDbContexts.Users
                .Include(u => u.QuizProfile)
                .ThenInclude(qu => qu.Points)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPost("{id}")]
        public IActionResult AddPointsAndProfile(int id, [FromBody] UserDto userDto)
        {
            var user = _quizDbContexts.Users
                .Include(u => u.QuizProfile)
                    .ThenInclude(qp => qp.Points)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound("User not found.");
            }


            if (userDto.QuizProfile != null && userDto.QuizProfile.Any())
            {
                if (user.QuizProfile == null)
                {
                    user.QuizProfile = new List<QuizProfile>();
                }

                user.QuizProfile.AddRange(userDto.QuizProfile.Select(profileDto => new QuizProfile
                {
                    Category = profileDto.Category,
                    Rating = profileDto.Rating,
                    Title = profileDto.Title,
                    Points = profileDto.Points?.Select(pointsDto => new Points
                    {
                        Score = pointsDto.Score
                    }).ToList()
                }));
            }

            _quizDbContexts.SaveChanges();

            return Ok(new { message = "Successfully added Points and QuizProfile for the user." });
        }
        /*
        [HttpPost("{id}")]
        public IActionResult AddPointsAndProfile(int id, [FromBody] UserDto userDto)
        {
            var user = _quizDbContexts.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (userDto.Points != null && userDto.Points.Any())
            {
                if (user.Points == null)
                {
                    user.Points = new List<Points>();
                }

                user.Points.AddRange(userDto.Points.Select(pointsDto => new Points { Score = pointsDto.Score }));
            }

            if (userDto.QuizProfile != null && userDto.QuizProfile.Any())
            {
                if (user.QuizProfile == null)
                {
                    user.QuizProfile = new List<QuizProfile>();
                }

                user.QuizProfile.AddRange(userDto.QuizProfile.Select(profileDto => new QuizProfile
                {
                    Category = profileDto.Category,
                    Rating = profileDto.Rating,
                    Title = profileDto.Title
                }));
            }

            _quizDbContexts.SaveChanges();

            return Ok(new { message = "Successfully added Points and QuizProfile for the user." });
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var allUsers = _quizDbContexts.Users
                .Include(q => q.Points)
                .Include(q => q.QuizProfile)
                //.AsSplitQuery()
                .ToList();

            if (allUsers == null || allUsers.Count == 0)
            {
                return NotFound("No quizzes found.");
            }

            return Ok(allUsers);
        }
        */
    }
}
