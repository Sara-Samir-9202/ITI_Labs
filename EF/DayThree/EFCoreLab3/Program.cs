
using EFCoreLab3.Models;
using EFCoreLab3.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EFCoreLab3
{
    class Program
    {
        static void Main(string[] args)
        {
        
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ITIDbContext>(options =>
                    options.UseSqlServer("Server=SARA-ELGHAZALLY\\MSSSQLSERVER;Database=ITI_DB;Trusted_Connection=True;TrustServerCertificate=True;"))
                .AddScoped<StudentService>()
                .BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var studentService = scope.ServiceProvider.GetRequiredService<StudentService>();

            studentService.AddStudent(new Student
            {
                StFname = "Sara",
                StLname = "ElGhazally",
                StAge = 22,
                DeptId = 1,
                StAddress = "Giza"
            });
            Console.WriteLine("Inserted!");

            var students = studentService.GetAllStudents();
            foreach (var s in students)
            {
                Console.WriteLine($"{s.StId} - {s.StFname} {s.StLname} - {s.StAge} - {s.StAddress} - Dept: {s.DeptId}");
            }

            studentService.UpdateStudent(1, "UpdatedF", "UpdatedL", 30, 2, "Cairo");
            Console.WriteLine("Updated!");

       
            studentService.DeleteStudent(2);
            Console.WriteLine("Deleted!");
        }
    }
}