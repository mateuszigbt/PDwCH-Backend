namespace Quiz.Model
{
    public class QuizProfile
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int Rating { get; set; }
        public List<Points>? Points { get; set; }
    }
}
