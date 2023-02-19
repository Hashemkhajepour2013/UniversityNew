using System.ComponentModel.DataAnnotations;

namespace University.Services.Students.Contracts.Dtos;

public sealed class AddStudentDto
{
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; } = null!;
    
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; } = null!;
    
    [MaxLength(11)]
    [Required]
    public string Mobile { get; set; } = null!;
    
    [MaxLength(10)]
    [Required]
    public string StudentNumber { get; set; }
}