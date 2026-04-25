using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLab02_Part02
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 2, 4, 6, 7, 1, 4, 2, 9, 1 };

            // Query1: Display numbers without repeated and sorted
            var uniqueSortedNumbers = numbers.Distinct().OrderBy(n => n);
            Console.WriteLine("Query1: Numbers without repetition and sorted:");
            foreach (var n in uniqueSortedNumbers)
            {
                Console.WriteLine(n);
            }

            // Query2: Show each number and its multiplication
            Console.WriteLine("\nQuery2: Each number and its multiplication:");
            foreach (var n in uniqueSortedNumbers)
            {
                Console.WriteLine($"{n} * 2 = {n * 2}");
            }

            string[] names = { "Tom", "Dick", "Harry", "MARY", "Jay" };

            // Query1: Select names with length = 3
            var namesLength3QueryExp = from name in names
                                       where name.Length == 3
                                       select name;
            var namesLength3Method = names.Where(n => n.Length == 3);
            Console.WriteLine("\nNames with length 3:");
            foreach (var n in namesLength3QueryExp)
                Console.WriteLine(n);

            // Query2: Names containing 'a' or 'A', sorted by length
            var namesWithAQueryExp = from name in names
                                     where name.ToLower().Contains("a")
                                     orderby name.Length
                                     select name;
            var namesWithAMethod = names.Where(n => n.ToLower().Contains("a"))
                                        .OrderBy(n => n.Length);
            Console.WriteLine("\nNames containing 'a' sorted by length:");
            foreach (var n in namesWithAQueryExp)
                Console.WriteLine(n);

            // Query3: First 2 names
            var first2Names = names.Take(2);
            Console.WriteLine("\nFirst 2 names:");
            foreach (var n in first2Names)
                Console.WriteLine(n);

            List<Student> students = new List<Student>()
            {
                new Student(){ ID=1, FirstName="Ali", LastName="Mohammed",
                    Subjects=new Subject[]{
                        new Subject(){ Code=22, Name="EF"},
                        new Subject(){ Code=33, Name="UML"}
                    }
                },
                new Student(){ ID=2, FirstName="Mona", LastName="Gala",
                    Subjects=new Subject[]{
                        new Subject(){ Code=22, Name="EF"},
                        new Subject(){ Code=34, Name="XML"},
                        new Subject(){ Code=25, Name="JS"}
                    }
                },
                new Student(){ ID=3, FirstName="Yara", LastName="Yousf",
                    Subjects=new Subject[]{
                        new Subject(){ Code=22, Name="EF"},
                        new Subject(){ Code=25, Name="JS"}
                    }
                },
                new Student(){ ID=4, FirstName="Ali", LastName="Ali",
                    Subjects=new Subject[]{
                        new Subject(){ Code=33, Name="UML"}
                    }
                }
            };

            // Query1: Full name and number of subjects
            Console.WriteLine("\nStudents and number of subjects:");
            var studentSubjectsCount = students.Select(s => new
            {
                FullName = s.FirstName + " " + s.LastName,
                SubjectsCount = s.Subjects.Length
            });
            foreach (var s in studentSubjectsCount)
            {
                Console.WriteLine($"{s.FullName} - Subjects: {s.SubjectsCount}");
            }

            // Query2: Order by FirstName Desc, LastName Asc
            Console.WriteLine("\nStudents ordered by FirstName Desc, LastName Asc:");
            var orderedStudents = students.OrderByDescending(s => s.FirstName)
                                          .ThenBy(s => s.LastName)
                                          .Select(s => new { s.FirstName, s.LastName });
            foreach (var s in orderedStudents)
            {
                Console.WriteLine($"{s.FirstName} {s.LastName}");
            }

            // Query3: Each student and student's subjects using SelectMany
            Console.WriteLine("\nEach student and their subjects:");
            var studentSubjects = students.SelectMany(s => s.Subjects, (s, sub) => new
            {
                StudentName = s.FirstName + " " + s.LastName,
                SubjectName = sub.Name
            });
            foreach (var s in studentSubjects)
            {
                Console.WriteLine($"{s.StudentName} - {s.SubjectName}");
            }

            // GroupBy (Subjects per student)
            Console.WriteLine("\nGroupBy: Subjects per student:");
            var groupedByStudent = students.Select(s => new
            {
                StudentName = s.FirstName + " " + s.LastName,
                Subjects = s.Subjects.Select(sub => sub.Name)
            });
            foreach (var g in groupedByStudent)
            {
                Console.WriteLine($"{g.StudentName} -> {string.Join(", ", g.Subjects)}");
            }

            Console.ReadLine();
        }
    }
}
