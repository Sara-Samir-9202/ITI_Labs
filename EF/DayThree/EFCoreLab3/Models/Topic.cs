using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLab3.Models;

public partial class Topic
{
    [Key]
    [Column("Top_Id")]
    public int TopId { get; set; }

    [Column("Top_Name")]
    public string? TopName { get; set; }

    [InverseProperty("Top")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
