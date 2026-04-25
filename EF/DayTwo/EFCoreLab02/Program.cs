using EFCoreLab02.Data;
using EFCoreLab02.Models;

class Program
{
    static void Main()
    {
        var context = new ITIDbContext();

        // Seed Departments
        if (!context.Departments.Any(d => d.Dept_Name == "Computer Science"))
        {
            var dept1 = new Department { Dept_Name = "Computer Science", Dept_Desc = "CS Dept", Dept_Location = "Building A" };
            context.Departments.Add(dept1);
        }

        context.SaveChanges();

        // Seed Topics
        if (!context.Topics.Any(t => t.Top_Name == "Programming"))
        {
            var topic1 = new Topic { Top_Name = "Programming" };
            context.Topics.Add(topic1);
        }
        context.SaveChanges();

        var studentService = new StudentService(context);
        var courseService = new CourseService(context);
        var instructorService = new InstructorService(context);

        // Student CRUD
        if (!context.Students.Any(s => s.St_Fname == "Sara" && s.St_Lname == "Samir"))
        {
            var newStudent = new Student
            {
                St_Fname = "Sara",
                St_Lname = "Samir",
                St_Age = 22,
                Dept_Id = context.Departments.First(d => d.Dept_Name == "Computer Science").Dept_Id
            };
            studentService.AddStudent(newStudent);
        }

      
        var allStudents = studentService.GetAllStudents();
        Console.WriteLine("Students:");
        foreach (var s in allStudents)
            Console.WriteLine($"{s.St_Id} - {s.St_Fname} {s.St_Lname} ({s.Department?.Dept_Name})");

        // Course CRUD
        if (!context.Courses.Any(c => c.Crs_Name == "EF Core"))
        {
            var newCourse = new Course
            {
                Crs_Name = "EF Core",
                Crs_Duration = 40,
                Top_Id = context.Topics.First(t => t.Top_Name == "Programming").Top_Id
            };
            courseService.AddCourse(newCourse);
        }

       
        var courses = courseService.GetAllCourses();
        Console.WriteLine("\nCourses:");
        foreach (var c in courses)
            Console.WriteLine($"{c.Crs_Id} - {c.Crs_Name} ({c.Topic?.Top_Name})");

        // Instructor CRUD
        if (!context.Instructors.Any(i => i.Ins_Name == "Mona"))
        {
            var newInstructor = new Instructor
            {
                Ins_Name = "Mona",
                Ins_Degree = "MSc",
                Salary = 12000,
                Dept_Id = context.Departments.First(d => d.Dept_Name == "Computer Science").Dept_Id
            };
            instructorService.AddInstructor(newInstructor);
        }

     
        var instructors = instructorService.GetAllInstructors();
        Console.WriteLine("\nInstructors:");
        foreach (var i in instructors)
            Console.WriteLine($"{i.Ins_Id} - {i.Ins_Name} ({i.Department?.Dept_Name})");
    }
}