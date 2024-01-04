using System.ComponentModel.DataAnnotations;

namespace Quiz.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; } 
        public bool? IsAdmin { get; set; }
        public List<QuizProfile>? QuizProfile { get; set; } 
    }
}
