using System.Collections.Generic;

namespace LINQ_Lab01
{
    public static class Repository
    {
        public static List<Student> Students = new List<Student>()
        {
            new Student{Id=1, FirstName="Ahmed", LastName="Ali", Age=22, Salary=3000, TrackId=1},
            new Student{Id=2, FirstName="Mohamed", LastName="Hassan", Age=35, Salary=6000, TrackId=1},
            new Student{Id=3, FirstName="Mona", LastName="Ibrahim", Age=28, Salary=4500, TrackId=2},
            new Student{Id=4, FirstName="Sara", LastName="Omar", Age=40, Salary=7000, TrackId=10},
            new Student{Id=5, FirstName="Mahmoud", LastName="Adel", Age=25, Salary=5000, TrackId=8}
        };
    }
}