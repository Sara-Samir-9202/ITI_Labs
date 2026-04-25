using System.Collections.Generic;

namespace LINQLab02_Part03
{
    public static class SampleData
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book{ISBN="1", Title="C#", Price=30, PublisherId=1, SubjectId=1},
            new Book{ISBN="2", Title="Java", Price=20, PublisherId=2, SubjectId=1},
            new Book{ISBN="3", Title="SQL", Price=40, PublisherId=1, SubjectId=2},
            new Book{ISBN="4", Title="Python", Price=15, PublisherId=3, SubjectId=1},
            new Book{ISBN="5", Title="AI", Price=50, PublisherId=2, SubjectId=3}
        };

        public static List<Publisher> Publishers = new List<Publisher>()
        {
            new Publisher{PublisherId=1, Name="TechBooks"},
            new Publisher{PublisherId=2, Name="CodePress"},
            new Publisher{PublisherId=3, Name="AI House"}
        };

        public static List<Subject> Subjects = new List<Subject>()
        {
            new Subject{SubjectId=1, Name="Programming"},
            new Subject{SubjectId=2, Name="Database"},
            new Subject{SubjectId=3, Name="AI"}
        };
    }
}