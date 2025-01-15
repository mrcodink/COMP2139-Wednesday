using System.ComponentModel.DataAnnotations;

namespace COMP2139_ICE.Models;

public class Project
{
    /// <summary>
    /// The unique primary key for projects
    /// </summary>
    public int ProjectId { get; set; }
    /// <summary>
    /// The name of the project
    /// Required - Ensures the property must be provided (must have a value)
    /// </summary>
    
    
    [Required]
    public required string Name { get; set; }
    
    
    public string? Description { get; set; }/// can be null or not null
    
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    
    public string? Status { get; set; }
    
    
    
}