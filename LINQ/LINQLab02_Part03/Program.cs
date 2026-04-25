using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQLab02_Part03
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1 - Display book title and ISBN
            var q1 = SampleData.Books
                .Select(b => new { b.Title, b.ISBN });

            // 2 - First 3 books with price > 25
            var q2 = SampleData.Books
                .Where(b => b.Price > 25)
                .Take(3);

            // 3 - Book title + publisher name
            var q3 = SampleData.Books
                .Join(SampleData.Publishers,
                      b => b.PublisherId,
                      p => p.PublisherId,
                      (b, p) => new { b.Title, PublisherName = p.Name });

            // 4 - Count of books with price > 20
            var q4 = SampleData.Books.Count(b => b.Price > 20);

            // 5 - Book title, price, subject name sorted
            var q5 = SampleData.Books
                .Join(SampleData.Subjects,
                      b => b.SubjectId,
                      s => s.SubjectId,
                      (b, s) => new { b.Title, b.Price, SubjectName = s.Name })
                .OrderBy(x => x.SubjectName)
                .ThenByDescending(x => x.Price);

            // 6 - Subjects with related books (2 methods)
            // Method Syntax
            var q6_method = SampleData.Subjects
                .GroupJoin(SampleData.Books,
                           s => s.SubjectId,
                           b => b.SubjectId,
                           (s, books) => new { s.Name, Books = books });

            // Query Syntax
            var q6_query = from s in SampleData.Subjects
                           join b in SampleData.Books
                           on s.SubjectId equals b.SubjectId into sb
                           select new { s.Name, Books = sb };

            // 7 - Books grouped by Publisher & Subject
            var q7 = SampleData.Books
                .Join(SampleData.Publishers,
                      b => b.PublisherId,
                      p => p.PublisherId,
                      (b, p) => new { b, PublisherName = p.Name })
                .Join(SampleData.Subjects,
                      x => x.b.SubjectId,
                      s => s.SubjectId,
                      (x, s) => new
                      {
                          x.PublisherName,
                          SubjectName = s.Name,
                          x.b.Title
                      })
                .GroupBy(x => new { x.PublisherName, x.SubjectName });

            // Display
            Print("Q1 - Title & ISBN", q1);
            Print("Q2 - First 3 books with price > 25", q2);
            Print("Q3 - Book title + Publisher", q3);

            Console.WriteLine($"\nQ4 - Count of books with price > 20: {q4}");

            Print("Q5 - Books sorted by Subject & Price", q5);

            PrintGroupWithBooks("Q6 Method Syntax - Subjects with Books", q6_method);
            PrintGroupWithBooks("Q6 Query Syntax - Subjects with Books", q6_query);

            PrintComplexGroup("Q7 - Books grouped by Publisher & Subject", q7);

            Console.ReadLine();
        }

        // Generic print for anonymous objects
        static void Print(string title, IEnumerable<object> list)
        {
            Console.WriteLine($"\n{title}");
            foreach (var item in list)
                Console.WriteLine(item);
        }

        // Print subjects with related books
        static void PrintGroupWithBooks(string title, IEnumerable<dynamic> list)
        {
            Console.WriteLine($"\n{title}");
            foreach (var x in list)
            {
                Console.WriteLine($"Subject: {x.Name}");
                foreach (var b in x.Books)
                    Console.WriteLine($"  {b.Title}");
            }
        }

        // Print books grouped by Publisher & Subject
        static void PrintComplexGroup(string title, IEnumerable<IGrouping<dynamic, dynamic>> groups)
        {
            Console.WriteLine($"\n{title}");
            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Key.PublisherName} - {g.Key.SubjectName}");
                foreach (var item in g)
                    Console.WriteLine($"  {item.Title}");
            }
        }
    }
}