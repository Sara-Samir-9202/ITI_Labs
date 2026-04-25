using System;
using System.Linq;

namespace LINQ_Lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1
            Console.WriteLine("Q1: All Students (Query Expression)");
            var q1 = from s in Repository.Students
                     select s;

            PrintStudents(q1);

            // 2
            Console.WriteLine("\nQ2: All Students (Method Syntax)");
            var q2 = Repository.Students.ToList();

            PrintStudents(q2);

            // 3
            Console.WriteLine("\nQ3: Students with Age > 30");
            var q3 = from s in Repository.Students
                     where s.Age > 30
                     select s;

            PrintStudents(q3);

            // 4
            Console.WriteLine("\nQ4: Students with Salary < 5000");
            var q4 = Repository.Students
                .Where(s => s.Salary < 5000);

            PrintStudents(q4);

            // 5
            Console.WriteLine("\nQ5: TrackId=1 && Salary>4000 Order Desc");
            var q5 = from s in Repository.Students
                     where s.TrackId == 1 && s.Salary > 4000
                     orderby s.FirstName descending
                     select s;

            PrintStudents(q5);

            // 6
            Console.WriteLine("\nQ6: TrackId=1 && Name contains 'm' Order by Salary ASC");
            var q6 = Repository.Students
                .Where(s => s.TrackId == 1 && s.FirstName.Contains("m"))
                .OrderBy(s => s.Salary);

            PrintStudents(q6);

            // 7
            Console.WriteLine("\nQ7: First Student Salary > 5000");
            var firstStudent = Repository.Students.First(s => s.Salary > 5000);
            var firstOrDefaultStudent = Repository.Students.FirstOrDefault(s => s.Salary > 5000);

            PrintSingleStudent(firstStudent);
            PrintSingleStudent(firstOrDefaultStudent);

            // 8
            Console.WriteLine("\nQ8: Last Student in Track 10");
            var lastStudent = Repository.Students.Last(s => s.TrackId == 10);
            var lastOrDefaultStudent = Repository.Students.LastOrDefault(s => s.TrackId == 10);

            PrintSingleStudent(lastStudent);
            PrintSingleStudent(lastOrDefaultStudent);

            // 9
            Console.WriteLine("\nQ9: Student Age = 25");
            var singleStudent = Repository.Students.Single(s => s.Age == 25);
            var singleOrDefaultStudent = Repository.Students.SingleOrDefault(s => s.Age == 25);

            PrintSingleStudent(singleStudent);
            PrintSingleStudent(singleOrDefaultStudent);

            // 10
            Console.WriteLine("\nQ10: Student TrackId = 8");
            var singleTrack = Repository.Students.Single(s => s.TrackId == 8);
            var singleOrDefaultTrack = Repository.Students.SingleOrDefault(s => s.TrackId == 8);

            PrintSingleStudent(singleTrack);
            PrintSingleStudent(singleOrDefaultTrack);

            // 11
            Console.WriteLine("\nQ11: Student at Index 4");
            var studentAtIndex = Repository.Students.ElementAt(4);
            PrintSingleStudent(studentAtIndex);

            // 12
            Console.WriteLine("\nQ12: Dynamic Sorting");
            FindStudentsSorted();

            Console.ReadLine();
        }

      
        static void PrintStudents(System.Collections.Generic.IEnumerable<Student> students)
        {
            foreach (var s in students)
            {
                Console.WriteLine($"{s.Id} - {s.FirstName} {s.LastName} - Age: {s.Age} - Salary: {s.Salary} - Track: {s.TrackId}");
            }
        }

     
        static void PrintSingleStudent(Student s)
        {
            if (s != null)
                Console.WriteLine($"{s.Id} - {s.FirstName} {s.LastName} - Age: {s.Age} - Salary: {s.Salary} - Track: {s.TrackId}");
            else
                Console.WriteLine("No Student Found");
        }

        static void FindStudentsSorted()
        {
            Console.WriteLine("Enter field (Name / Age / Salary): ");
            string field = Console.ReadLine().ToLower();

            Console.WriteLine("Enter order (ASC / DESC): ");
            string order = Console.ReadLine().ToLower();

            var students = Repository.Students.AsQueryable();

            if (field == "name")
            {
                students = (order == "asc") ?
                    students.OrderBy(s => s.FirstName) :
                    students.OrderByDescending(s => s.FirstName);
            }
            else if (field == "age")
            {
                students = (order == "asc") ?
                    students.OrderBy(s => s.Age) :
                    students.OrderByDescending(s => s.Age);
            }
            else if (field == "salary")
            {
                students = (order == "asc") ?
                    students.OrderBy(s => s.Salary) :
                    students.OrderByDescending(s => s.Salary);
            }

            Console.WriteLine("\nSorted Result:");
            PrintStudents(students);
        }
    }
}