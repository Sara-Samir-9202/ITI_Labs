using EFCoreLab02.Data;
using EFCoreLab02.Models;
using Microsoft.EntityFrameworkCore;

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
        return _context.Students.Include(s => s.Department).ToList();
    }

    
    public Student? GetStudentById(int id)
    {
        return _context.Students
            .Include(s => s.Department)
            .FirstOrDefault(s => s.St_Id == id);
    }

   

    public void UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        _context.SaveChanges();
    }

    public void DeleteStudent(int id)
    {
        var student = _context.Students.Find(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}