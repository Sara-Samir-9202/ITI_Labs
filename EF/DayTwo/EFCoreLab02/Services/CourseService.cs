using EFCoreLab02.Data;
using EFCoreLab02.Models;
using Microsoft.EntityFrameworkCore;

public class CourseService
{
    private readonly ITIDbContext _context;

    public CourseService(ITIDbContext context)
    {
        _context = context;
    }

    
    public void AddCourse(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
    }


    public List<Course> GetAllCourses()
    {
        return _context.Courses.Include(c => c.Topic).ToList();
    }


    public Course? GetCourseById(int id)
    {
        return _context.Courses
            .Include(c => c.Topic)
            .FirstOrDefault(c => c.Crs_Id == id);
    }

  
    public void UpdateCourse(Course course)
    {
        _context.Courses.Update(course);
        _context.SaveChanges();
    }


    public void DeleteCourse(int id)
    {
        var course = _context.Courses.Find(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
    }
}