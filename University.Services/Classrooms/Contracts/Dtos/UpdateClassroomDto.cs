using System.ComponentModel.DataAnnotations;

namespace University.Services.Classrooms.Contracts.Dtos;

public sealed class UpdateClassroomDto
{
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    [Range(2, 30)]
    public byte Capacity { get; set; }

    [Required]
    public int TermId { get; set; }

    [Required]
    public int LessonId { get; set; }
    
    [Required]
    public int ProfessorId { get; set; }
}