using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLab03.Models;

[Index("DeptId", Name = "IX_Instructors_Dept_Id")]
public partial class Instructor
{
    [Key]
    [Column("Ins_Id")]
    public int InsId { get; set; }

    [Column("Ins_Name")]
    public string? InsName { get; set; }

    [Column("Ins_Degree")]
    public string? InsDegree { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Salary { get; set; }

    [Column("Dept_Id")]
    public int DeptId { get; set; }

    [ForeignKey("DeptId")]
    [InverseProperty("Instructors")]
    public virtual Department Dept { get; set; } = null!;

    [InverseProperty("Ins")]
    public virtual ICollection<InsCourse> InsCourses { get; set; } = new List<InsCourse>();
}
