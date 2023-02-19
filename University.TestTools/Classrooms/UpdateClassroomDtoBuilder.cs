using University.Services.Classrooms.Contracts.Dtos;

namespace University.TestTools.Classrooms;

public sealed class UpdateClassroomDtoBuilder
{
    private UpdateClassroomDto _dto;

    public UpdateClassroomDtoBuilder()
    {
        _dto = new UpdateClassroomDto
        {
        };
    }

    public UpdateClassroomDtoBuilder WithStartDate(DateTime startDate)
    {
        _dto.StartDate = startDate;
        return this;
    }

    public UpdateClassroomDtoBuilder WithEndDate(DateTime endDate)
    {
        _dto.EndDate = endDate;
        return this;
    }

    public UpdateClassroomDtoBuilder WithCapacity(byte capacity)
    {
        _dto.Capacity = capacity;
        return this;
    }
    
    public UpdateClassroomDtoBuilder WithProfessor(int professorId)
    {
        _dto.ProfessorId = professorId;
        return this;
    }

    public UpdateClassroomDtoBuilder WithLesson(int lessonId)
    {
        _dto.LessonId = lessonId;
        return this;
    }

    public UpdateClassroomDtoBuilder WithTerm(int termId)
    {
        _dto.TermId = termId;
        return this;
    }

    public UpdateClassroomDto Build()
    {
        return _dto;
    }
}