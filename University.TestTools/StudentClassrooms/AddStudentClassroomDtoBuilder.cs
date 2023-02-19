using University.Services.StudentClassrooms.Contracts.Dtos;

namespace University.TestTools.StudentClassrooms;

public sealed class AddStudentClassroomDtoBuilder
{
    private readonly AddStudentClassroomDto _dto;

    public AddStudentClassroomDtoBuilder()
    {
        _dto = new AddStudentClassroomDto();
    }

    public AddStudentClassroomDtoBuilder WithStudent(int studentId)
    {
        _dto.StudentId = studentId;
        return this;
    }

    public AddStudentClassroomDtoBuilder WithClassroom(int classroomId)
    {
        _dto.ClassroomId = classroomId;
        return this;
    }

    public AddStudentClassroomDto Build()
    {
        return _dto;
    }
}