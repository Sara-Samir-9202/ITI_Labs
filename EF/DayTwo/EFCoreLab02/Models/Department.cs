namespace EFCoreLab02.Models
{
    public class Department
    {
        public int Dept_Id { get; set; }
        public string? Dept_Name { get; set; }
        public string? Dept_Desc { get; set; }
        public string? Dept_Location { get; set; }

        public int? Dept_Manager { get; set; }
        public DateTime? Manager_hiredate { get; set; }

        public ICollection<Student> Students { get; set; }
            = new List<Student>();

        public ICollection<Instructor> Instructors { get; set; }
            = new List<Instructor>();
    }
}