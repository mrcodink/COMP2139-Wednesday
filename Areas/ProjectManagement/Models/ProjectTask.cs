﻿using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Areas.ProjectManagement.Models;

public class ProjectTask
{
    [Key]
    public int ProjectTaskId { get; set; }
    
    [Display(Name = "Task Title")]
    [Required]
    [StringLength(100, ErrorMessage = "Task Name cannot be longer than 100 characters.")]
    public required string Title { get; set; }
    
    [Display(Name = "Task Description")]
    [Required]
    [DataType(DataType.MultilineText)]
    [StringLength(500, ErrorMessage = "Task Description cannot be longer than 500 characters.")]
    public required string Description { get; set; }
    
    // Foreign Key
    [Display(Name = "Parent Project Id")]
    public int ProjectId { get; set; }
    
    // Navigation Property
    [Display(Name = "Parent Project")]
    public Project? Project { get; set; }
    
    
    
}