using University.Services.Classrooms.Contracts.Dtos;

namespace University.TestTools.Classrooms;

public sealed class CreateClassroomDtoBuilder
{
    private AddClassroomDto _dto;

    public CreateClassroomDtoBuilder()
    {
        _dto = new AddClassroomDto
        {
        };
    }

    public CreateClassroomDtoBuilder WithStartDate(DateTime startDate)
    {
        _dto.StartDate = startDate;
        return this;
    }

    public CreateClassroomDtoBuilder WithEndDate(DateTime endDate)
    {
        _dto.EndDate = endDate;
        return this;
    }

    public CreateClassroomDtoBuilder WithCapacity(byte capacity)
    {
        _dto.Capacity = capacity;
        return this;
    }

    public CreateClassroomDtoBuilder WithTerm(int termId)
    {
        _dto.TermId = termId;
        return this;
    }
    
    public CreateClassroomDtoBuilder WithLesson(int LessonId)
    {
        _dto.LessonId = LessonId;
        return this;
    }

    public CreateClassroomDtoBuilder WithProfessor(int professorId)
    {
        _dto.ProfessorId = professorId;
        return this;
    }
    public AddClassroomDto Build()
    {
        return _dto;
    }
}