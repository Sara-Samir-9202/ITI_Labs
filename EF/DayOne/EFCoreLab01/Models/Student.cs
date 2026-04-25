using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreLab01.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string? Name { get; set; }

        public int Age { get; set; }         
        public decimal Salary { get; set; }  


        public int DepartmentId { get; set; }

        // Navigation Property
        public Department? Department { get; set; }
    }
}
