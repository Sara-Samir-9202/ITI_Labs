using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLab3.Models;

public partial class Department
{
    [Key]
    [Column("Dept_Id")]
    public int DeptId { get; set; }

    [Column("Dept_Name")]
    public string? DeptName { get; set; }

    [Column("Dept_Desc")]
    public string? DeptDesc { get; set; }

    [Column("Dept_Location")]
    public string? DeptLocation { get; set; }

    [Column("Dept_Manager")]
    public int? DeptManager { get; set; }

    [Column("Manager_hiredate")]
    public DateTime? ManagerHiredate { get; set; }

    [InverseProperty("Dept")]
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    [InverseProperty("Dept")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
