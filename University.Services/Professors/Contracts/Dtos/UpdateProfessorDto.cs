using System.ComponentModel.DataAnnotations;

namespace University.Services.Professors.Contracts.Dtos;

public sealed class UpdateProfessorDto
{
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; } = null!;
    
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; } = null!;
    
    [MaxLength(10)]
    [Required]
    public string PersonnelId { get; set; }
}