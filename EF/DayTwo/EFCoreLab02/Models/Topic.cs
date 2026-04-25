namespace EFCoreLab02.Models
{
    public class Topic
    {
        public int Top_Id { get; set; }
        public string? Top_Name { get; set; }

        public ICollection<Course> Courses { get; set; }
            = new List<Course>();
    }
}