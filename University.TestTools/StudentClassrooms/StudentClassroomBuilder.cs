using University.Entities;

namespace University.TestTools.StudentClassrooms;

public class StudentClassroomBuilder
{
    private readonly StudentClassroom _studentClassroom;

    public StudentClassroomBuilder()
    {
        _studentClassroom = new StudentClassroom();
    }

    public StudentClassroomBuilder WithStudent(int studentId)
    {
        _studentClassroom.StudentId = studentId;
        return this;
    }

    public StudentClassroomBuilder WithClassroom(int classroomId)
    {
        _studentClassroom.ClassroomId = classroomId;
        return this;
    }
    
    public StudentClassroom Build()
    {
        return _studentClassroom;
    }
}