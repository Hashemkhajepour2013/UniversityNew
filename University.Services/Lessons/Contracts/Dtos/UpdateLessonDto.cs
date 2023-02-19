using System.ComponentModel.DataAnnotations;

namespace University.Services.Lessons.Contracts.Dtos;

public sealed class UpdateLessonDto
{
    [MaxLength(100)] 
    [Required] 
    public string Title { get; set; } = null!;
    
    [Required]
    public byte Coefficient { get; set; }
}