using EFCoreLab02.Data;
using EFCoreLab02.Models;
using Microsoft.EntityFrameworkCore;

public class InstructorService
{
    private readonly ITIDbContext _context;

    public InstructorService(ITIDbContext context)
    {
        _context = context;
    }


    public void AddInstructor(Instructor instructor)
    {
        _context.Instructors.Add(instructor);
        _context.SaveChanges();
    }

    
    public List<Instructor> GetAllInstructors()
    {
        return _context.Instructors.Include(i => i.Department).ToList();
    }


    public Instructor? GetInstructorById(int id)
    {
        return _context.Instructors
            .Include(i => i.Department)
            .FirstOrDefault(i => i.Ins_Id == id);
    }

  
    public void UpdateInstructor(Instructor instructor)
    {
        _context.Instructors.Update(instructor);
        _context.SaveChanges();
    }

    public void DeleteInstructor(int id)
    {
        var instructor = _context.Instructors.Find(id);
        if (instructor != null)
        {
            _context.Instructors.Remove(instructor);
            _context.SaveChanges();
        }
    }
}