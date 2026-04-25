using System;
using System.Collections.Generic;
using System.Linq;
using EFCoreLab01.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLab01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EFCore Lab 1&2");

            using (var context = new AppDbContext())
            {
                // Part 01
                Console.WriteLine("\n Part 01");
                // 1.Insert 5 Departments.
                if (!context.Departments.Any())
                {
                    var departments = new List<Department>()
                    {
                        new Department { Name = "HR" },
                        new Department { Name = "IT" },
                        new Department { Name = "Sales" },
                        new Department { Name = "Finance" },
                        new Department { Name = "Marketing" }
                    };
                    context.Departments.AddRange(departments);
                    context.SaveChanges();
                    Console.WriteLine("Departments inserted successfully!");
                }
                else
                {
                    Console.WriteLine("Departments already exist, skipping insert.");
                }

                // 2.Insert 10 Students
                if (!context.Students.Any())
                {
                    var students = new List<Student>()
                    {
                        new Student { Name = "Ali", DepartmentId = 1, Age=28, Salary=4500 },
                        new Student { Name = "Omar", DepartmentId = 1, Age=35, Salary=5500 },
                        new Student { Name = "Sara", DepartmentId = 2, Age=32, Salary=4800 },
                        new Student { Name = "Mona", DepartmentId = 2, Age=29, Salary=5100 },
                        new Student { Name = "Hassan", DepartmentId = 3, Age=40, Salary=6000 },
                        new Student { Name = "Yara", DepartmentId = 3, Age=27, Salary=4200 },
                        new Student { Name = "Khaled", DepartmentId = 4, Age=33, Salary=3900 },
                        new Student { Name = "Nour", DepartmentId = 4, Age=31, Salary=4700 },
                        new Student { Name = "Salma", DepartmentId = 5, Age=26, Salary=4300 },
                        new Student { Name = "Mostafa", DepartmentId = 5, Age=38, Salary=5200 }
                    };
                    context.Students.AddRange(students);
                    context.SaveChanges();
                    Console.WriteLine("Students inserted successfully!");
                }
                else
                {
                    Console.WriteLine("Students already exist, skipping insert.");
                }



                // 3.Display all Students.
                Console.WriteLine("\nAll Students:");
                foreach (var s in context.Students.ToList())
                    Console.WriteLine($"Id: {s.Id}, Name: {s.Name}");

                // 4.Display all Departments.
                Console.WriteLine("\nAll Departments:");
                foreach (var d in context.Departments.ToList())
                    Console.WriteLine($"Id: {d.Id}, Name: {d.Name}");

                // 5.Display Students With Department Name. [Using Include]
                Console.WriteLine("\nStudents with Department Name:");
                foreach (var s in context.Students.Include(s => s.Department).ToList())
                    Console.WriteLine($"{s.Name} - {s.Department!.Name}");

                // 6.Display Students With Department Name in Department Id = 1. [Using Include]
                Console.WriteLine("\nStudents in DepartmentId = 1:");
                foreach (var s in context.Students.Include(s => s.Department)
                                                 .Where(s => s.DepartmentId == 1).ToList())
                    Console.WriteLine($"{s.Name} - {s.Department!.Name}");

                // 7.Display all Students with DeptId =1 OrderBy Name descending.
                Console.WriteLine("\nStudents in DepartmentId = 1 ordered by Name descending:");
                foreach (var s in context.Students
                                         .Where(s => s.DepartmentId == 1)
                                         .OrderByDescending(s => s.Name)
                                         .ToList())
                    Console.WriteLine(s.Name);



                Console.WriteLine("\n Part 02");

                // 1. Display all Students (Query Expression)
                Console.WriteLine("\n1. All Students (Query Expression):");
                var studentsQuery = from s in context.Students select s;
                foreach (var s in studentsQuery)
                    Console.WriteLine($"{s.Name} - Age: {s.Age} - Salary: {s.Salary}");

                // 2. Display all Students (Method Syntax)
                Console.WriteLine("\n2. All Students (Method Syntax):");
                var studentsMethod = context.Students.ToList();
                foreach (var s in studentsMethod)
                    Console.WriteLine($"{s.Name} - Age: {s.Age} - Salary: {s.Salary}");

                // 3. Students with Age > 30 (Query Expression)
                Console.WriteLine("\n3. Students with Age > 30 (Query Expression):");
                var ageQuery = from s in context.Students where s.Age > 30 select s;
                foreach (var s in ageQuery)
                    Console.WriteLine($"{s.Name} - Age: {s.Age}");

                // 4. Students with Salary < 5000 (Method Syntax)
                Console.WriteLine("\n4. Students with Salary < 5000 (Method Syntax):");
                var salaryMethod = context.Students.Where(s => s.Salary < 5000).ToList();
                foreach (var s in salaryMethod)
                    Console.WriteLine($"{s.Name} - Salary: {s.Salary}");

                // 5. DeptId=1 & Salary>4000 ordered by Name Desc (Query Expression)
                Console.WriteLine("\n5. DeptId=1 & Salary>4000 ordered by Name Desc (Query Expression):");
                var deptQuery = from s in context.Students
                                where s.DepartmentId == 1 && s.Salary > 4000
                                orderby s.Name descending
                                select s;
                foreach (var s in deptQuery)
                    Console.WriteLine($"{s.Name} - Salary: {s.Salary}");

                // 6. DeptId=1 & Name contains 'm', OrderBy Salary Asc (Method Syntax)
                Console.WriteLine("\n6. DeptId=1 & Name contains 'm', OrderBy Salary Asc (Method Syntax):");
                var deptMethod = context.Students
                                        .Where(s => s.DepartmentId == 1 && s.Name!.Contains("m"))
                                        .OrderBy(s => s.Salary)
                                        .ToList();
                foreach (var s in deptMethod)
                    Console.WriteLine($"{s.Name} - Salary: {s.Salary}");

                // 7. First Student with Salary > 5000
                Console.WriteLine("\n7. First Student with Salary > 5000:");
                var firstStudent = context.Students.FirstOrDefault(s => s.Salary > 5000);
                if (firstStudent != null)
                    Console.WriteLine($"{firstStudent.Name} - Salary: {firstStudent.Salary}");

                // 8. Last Student in Department 10
                Console.WriteLine("\n8. Last Student in Department 10:");
                var lastStudent = context.Students.Where(s => s.DepartmentId == 10).OrderBy(s => s.Id)
                         .LastOrDefault();
                if (lastStudent != null)
                    Console.WriteLine($"{lastStudent.Name}");
                else
                    Console.WriteLine("No Student in Department 10");

                // 9. Student with Age = 25
                Console.WriteLine("\n9. Student with Age = 25:");
                var student25 = context.Students.SingleOrDefault(s => s.Age == 25);
                if (student25 != null)
                    Console.WriteLine($"{student25.Name}");
                else
                    Console.WriteLine("No Student with Age 25");

                // 10. Student with DepartmentId = 8
                Console.WriteLine("\n10. Student with DepartmentId = 8:");
                var studentDept8 = context.Students.SingleOrDefault(s => s.DepartmentId == 8);
                if (studentDept8 != null)
                    Console.WriteLine($"{studentDept8.Name}");
                else
                    Console.WriteLine("No Student in Department 8");

                Console.WriteLine("\nAll operations completed!");
            }
        }
    }
}