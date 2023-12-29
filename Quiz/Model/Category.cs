using System.ComponentModel.DataAnnotations;

namespace Quiz.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string NameCategory { get; set; }
    }
}
