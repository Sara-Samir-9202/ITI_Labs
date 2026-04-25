using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLab3.Models;

[Index("TopId", Name = "IX_Courses_Top_Id")]
public partial class Course
{
    [Key]
    [Column("Crs_Id")]
    public int CrsId { get; set; }

    [Column("Crs_Name")]
    public string? CrsName { get; set; }

    [Column("Crs_Duration")]
    public int CrsDuration { get; set; }

    [Column("Top_Id")]
    public int TopId { get; set; }

    [InverseProperty("Crs")]
    public virtual ICollection<InsCourse> InsCourses { get; set; } = new List<InsCourse>();

    [InverseProperty("Crs")]
    public virtual ICollection<StudCourse> StudCourses { get; set; } = new List<StudCourse>();

    [ForeignKey("TopId")]
    [InverseProperty("Courses")]
    public virtual Topic Top { get; set; } = null!;
}
