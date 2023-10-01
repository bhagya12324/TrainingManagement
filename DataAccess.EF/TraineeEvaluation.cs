﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EF;

public partial class TraineeEvaluation
{
    [Key]
    public int TraineeEvaluationId { get; set; }

    public int? EvaluationId { get; set; }

    public int? TraineeId { get; set; }

    public double? Score { get; set; }

    [Column(TypeName = "text")]
    public string Remarks { get; set; }

    [ForeignKey("EvaluationId")]
    [InverseProperty("TraineeEvaluations")]
    public virtual Evaluation Evaluation { get; set; }

    [ForeignKey("TraineeId")]
    [InverseProperty("TraineeEvaluations")]
    public virtual Trainee Trainee { get; set; }
}