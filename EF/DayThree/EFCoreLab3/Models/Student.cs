using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLab3.Models;

[Index("DeptId", Name = "IX_Students_Dept_Id")]
public partial class Student
{
    [Key]
    [Column("St_Id")]
    public int StId { get; set; }

    [Column("St_Fname")]
    public string? StFname { get; set; }

    [Column("St_Lname")]
    public string? StLname { get; set; }

    [Column("St_Address")]
    public string? StAddress { get; set; }

    [Column("St_Age")]
    public int StAge { get; set; }

    [Column("Dept_Id")]
    public int DeptId { get; set; }

    [StringLength(20)]
    public string? StPhone { get; set; }

    [Column("St_Email")]
    [StringLength(50)]
    public string? StEmail { get; set; }

    [ForeignKey("DeptId")]
    [InverseProperty("Students")]
    public virtual Department Dept { get; set; } = null!;

    [InverseProperty("St")]
    public virtual ICollection<StudCourse> StudCourses { get; set; } = new List<StudCourse>();
}
