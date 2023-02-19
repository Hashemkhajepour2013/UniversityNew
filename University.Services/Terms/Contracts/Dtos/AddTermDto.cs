using System.ComponentModel.DataAnnotations;

namespace University.Services.Terms.Contracts.Dtos;

public sealed class AddTermDto
{
    [MaxLength(100)] 
    [Required]
    public string Title { get; set; } = null!;
    
    [Required]
    [Range(12, 20)]
    public byte UnitCount { get; set; }
}