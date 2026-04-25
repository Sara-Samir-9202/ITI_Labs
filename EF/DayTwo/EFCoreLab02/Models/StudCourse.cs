namespace EFCoreLab02.Models
{
    public class StudCourse
    {
        public int Crs_Id { get; set; }
        public int St_Id { get; set; }
        public string? Grade { get; set; }

        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}