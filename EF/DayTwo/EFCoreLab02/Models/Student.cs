namespace EFCoreLab02.Models
{
    public class Student
    {
        public int St_Id { get; set; }
        public string? St_Fname { get; set; }
        public string? St_Lname { get; set; }
        public string? St_Address { get; set; }
        public int St_Age { get; set; }

        public int Dept_Id { get; set; }

        public Department? Department { get; set; }

        public ICollection<StudCourse> StudCourses { get; set; }
            = new List<StudCourse>();
    }
}