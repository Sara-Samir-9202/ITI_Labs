namespace EFCoreLab02.Models
{
    public class InsCourse
    {
        public int Ins_Id { get; set; }
        public int Crs_Id { get; set; }
        public string? Evaluation { get; set; }

        public Instructor? Instructor { get; set; }
        public Course? Course { get; set; }
    }
}