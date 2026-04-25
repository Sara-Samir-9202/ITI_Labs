using EFCoreLab01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreLab01.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string? Name { get; set; }

        // One Department => Many Students
        public List<Student>? Students { get; set; }
    }
}