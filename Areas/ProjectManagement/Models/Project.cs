using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Areas.ProjectManagement.Models;

public class Project
{
    /// <summary>
    ///  The unique primary key for projects
    /// </summary>
    public int ProjectId { get; set; }
    
    /// <summary>
    /// The name of the project
    /// Required - Ensures the property must be provided (must have a value)
    /// </summary>
    [Display(Name = "Project Name")]
    [Required]
    [StringLength(100, ErrorMessage = "Project Name cannot be longer than 100 characters.")]
    public required string ProjectName { get; set; }
    
    [Display(Name = "Project Description")]
    [DataType(DataType.MultilineText)]
    [StringLength(500, ErrorMessage = "Project Description cannot be longer than 100 characters.")]
    public string? ProjectDescription { get; set; } // ? means optional
    
    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime ProjectStartDate { get; set; } 
    
    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime ProjectEndDate { get; set; } 
    
    [Display(Name = "Project Status")]
    public string? Status { get; set; } // ? means optional
    
    // This allows EF Core to understand that one Project has many ProjectTasks
    public List<ProjectTask>? Tasks { get; set; } = new();




}