using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLab3.Models;

[PrimaryKey("InsId", "CrsId")]
[Index("CrsId", Name = "IX_InsCourses_Crs_Id")]
public partial class InsCourse
{
    [Key]
    [Column("Ins_Id")]
    public int InsId { get; set; }

    [Key]
    [Column("Crs_Id")]
    public int CrsId { get; set; }

    public string? Evaluation { get; set; }

    [ForeignKey("CrsId")]
    [InverseProperty("InsCourses")]
    public virtual Course Crs { get; set; } = null!;

    [ForeignKey("InsId")]
    [InverseProperty("InsCourses")]
    public virtual Instructor Ins { get; set; } = null!;
}
