using Quiz.Model;

namespace Quiz.DTO_s
{
    public class QuizProfileDto
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public int Rating { get; set; }
        public List<PointsDto>? Points { get; set; }
    }
}
