using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQLab02_Part01
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1
            var q1 = Repository.Employees.Take(4);

            // 2
            var q2 = Repository.Employees
                .Where(e => e.Salary > 5000)
                .Take(3);

            // 3 
            var q3 = Repository.Employees
                .Skip(Math.Max(0, Repository.Employees.Count - 4));

            // 4
            var q4 = Repository.Employees
                .Skip(2)
                .Take(2);

            // 5
            var q5 = Repository.Employees
                .TakeWhile(e => e.Name.Length < 5);

            // 6
            var q6 = Repository.Employees
                .Distinct(new EmployeeComparer());

            // 7
            var q7 = Repository.Employees
                .Select(e => new { e.Id, e.Name });

            // 8
            var q8 = from e in Repository.Employees
                     select new { e.Id, e.Name };

            // 9
            var q9 = from e in Repository.Employees
                     join d in Repository.Departments
                     on e.DeptId equals d.DeptId
                     select new { e.Name, d.DeptName };

            // 10
            var q10 = Repository.Employees
                .Join(Repository.Departments,
                      e => e.DeptId,
                      d => d.DeptId,
                      (e, d) => new { e.Name, d.DeptName });

            // 11 
            var q11 = Repository.Employees
                .Join(Repository.Departments,
                      e => e.DeptId,
                      d => d.DeptId,
                      (e, d) => new { e, d.DeptName })
                .GroupBy(x => x.DeptName, x => x.e);

            // 12 
            var q12 = from e in Repository.Employees
                      join d in Repository.Departments
                      on e.DeptId equals d.DeptId
                      group e by d.DeptName;

            // 13
            var min = Repository.Employees.Min(e => e.Salary);
            var max = Repository.Employees.Max(e => e.Salary);
            var avg = Repository.Employees.Average(e => e.Salary);

            // 14
            var q14 = Repository.Employees
                .Where(e => e.Salary < avg);

            // 15
            List<int> list1 = new List<int> { 1, 2, 3, 4 };
            List<int> list2 = new List<int> { 3, 4, 5, 6 };

            var except = list1.Except(list2);
            var concat = list1.Concat(list2);
            var union = list1.Union(list2);
            var intersect = list1.Intersect(list2);

            // 16
            List<string> names = new List<string> { "Ali", "Mona", "Omar" };
            List<string> phones = new List<string> { "111", "222", "333" };

            var zipped = names.Zip(phones, (n, p) => new { Name = n, Phone = p });

            // Display
            Print("Q1", q1);
            Print("Q2", q2);
            Print("Q3", q3);
            Print("Q4", q4);
            Print("Q5", q5);
            Print("Q6", q6);

            PrintAnonymous("Q7", q7);
            PrintAnonymous("Q8", q8);
            PrintAnonymous("Q9", q9);
            PrintAnonymous("Q10", q10);

            PrintGroup("Q11", q11);
            PrintGroup("Q12", q12);

            Print("Q14", q14);

            Console.WriteLine($"\nMin Salary = {min}");
            Console.WriteLine($"Max Salary = {max}");
            Console.WriteLine($"Avg Salary = {avg}");

            PrintInts("Except", except);
            PrintInts("Concat", concat);
            PrintInts("Union", union);
            PrintInts("Intersect", intersect);

            PrintAnonymous("Zip", zipped);

            Console.ReadLine();
        }

        static void Print(string title, IEnumerable<Employee> list)
        {
            Console.WriteLine($"\n{title}");
            foreach (var e in list)
                Console.WriteLine($"{e.Id} - {e.Name} - {e.Salary}");
        }

        static void PrintAnonymous(string title, IEnumerable<object> list)
        {
            Console.WriteLine($"\n{title}");
            foreach (var item in list)
                Console.WriteLine(item);
        }

        static void PrintGroup(string title, IEnumerable<IGrouping<string, Employee>> groups)
        {
            Console.WriteLine($"\n{title}");
            foreach (var g in groups)
            {
                Console.WriteLine($"Department: {g.Key}");
                foreach (var e in g)
                    Console.WriteLine($"  {e.Name}");
            }
        }

        static void PrintInts(string title, IEnumerable<int> list)
        {
            Console.WriteLine($"\n{title}");
            foreach (var i in list)
                Console.WriteLine(i);
        }
    }
}