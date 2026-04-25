namespace EFCoreLab02.Models
{
    public class Course
    {
        public int Crs_Id { get; set; }
        public string? Crs_Name { get; set; }
        public int Crs_Duration { get; set; }

        public int Top_Id { get; set; }

        public Topic? Topic { get; set; }

        public ICollection<StudCourse> StudCourses { get; set; }
            = new List<StudCourse>();

        public ICollection<InsCourse> InsCourses { get; set; }
            = new List<InsCourse>();
    }
}