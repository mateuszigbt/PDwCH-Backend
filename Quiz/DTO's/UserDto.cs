using Quiz.Model;

namespace Quiz.DTO_s
{
    public class UserDto
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool? IsAdmin { get; set; }
        public List<QuizProfileDto>? QuizProfile { get; set; }
        public List<PointsDto>? Points { get; set; }
    }
}
