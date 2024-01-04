using Quiz.DTO_s.Profiles;
using Quiz.Model;

namespace Quiz.DTO_s
{
    public class QuizDto
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public CategoryDto Category { get; set; }
        public List<AnswerDto> Answers { get; set; }
        public List<CorrectAnswerDto> CorrectAnswers { get; set; }
        public List<QuestionDto> Questions { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}
