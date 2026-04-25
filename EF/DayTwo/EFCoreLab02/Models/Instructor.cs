namespace EFCoreLab02.Models
{
    public class Instructor
    {
        public int Ins_Id { get; set; }
        public string? Ins_Name { get; set; }
        public string? Ins_Degree { get; set; }
        public decimal Salary { get; set; }

        public int Dept_Id { get; set; }

        public Department? Department { get; set; }

        public ICollection<InsCourse> InsCourses { get; set; }
            = new List<InsCourse>();
    }
}