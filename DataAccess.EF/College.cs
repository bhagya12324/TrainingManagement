﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF;

public partial class College
{
    [Key]
    public int CollegeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CollegeName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Location { get; set; }

    [Column(TypeName = "text")]
    public string Remarks { get; set; }

    [InverseProperty("College")]
    public virtual ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
}