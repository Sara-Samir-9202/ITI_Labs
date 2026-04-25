
using EFCoreLab3.Models;

namespace EFCoreLab3.Services
{
    public class StudentService
    {
        private readonly ITIDbContext _context;

        public StudentService(ITIDbContext context)
        {
            _context = context;
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

    
        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

    
        public Student? GetStudentById(int id)
        {
            return _context.Students.FirstOrDefault(s => s.StId == id);
        }

      
        public void UpdateStudent(int id, string fname, string lname, int age, int deptId, string address)
        {
            var student = _context.Students.FirstOrDefault(s => s.StId == id);
            if (student != null)
            {
                student.StFname = fname;
                student.StLname = lname;
                student.StAge = age;
                student.DeptId = deptId;
                student.StAddress = address;
                _context.SaveChanges();
            }
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StId == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}