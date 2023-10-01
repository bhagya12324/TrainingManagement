﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF;

public partial class FeesDetail
{
    [Key]
    public int FeesId { get; set; }

    public int? BatchId { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Date { get; set; }

    public double? Amount { get; set; }

    [Column(TypeName = "text")]
    public string Remarks { get; set; }

    [ForeignKey("BatchId")]
    [InverseProperty("FeesDetails")]
    public virtual Batch Batch { get; set; }
}