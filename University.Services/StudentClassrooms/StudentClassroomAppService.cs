using University.Entities;
using University.Services.Classrooms.Contracts;
using University.Services.Classrooms.Extensions;
using University.Services.Professors.Contracts;
using University.Services.StudentClassrooms.Contracts;
using University.Services.StudentClassrooms.Contracts.Dtos;
using University.Services.StudentClassrooms.Extensions;
using University.Services.Students.Contracts;
using University.Services.Students.Extensions;

namespace University.Services.StudentClassrooms;

public class StudentClassroomAppService : StudentClassroomService
{
    private readonly StudentClassroomRepository _repository;
    private readonly StudentRepository _studentRepository;
    private readonly ProfessorRepository _professorRepository;
    private readonly ClassroomRepository _classroomRepository;
    private readonly UnitOfWork _unitOfWork;
    public StudentClassroomAppService(
        StudentClassroomRepository repository,
        StudentRepository studentRepository,
        ProfessorRepository professorRepository,
        ClassroomRepository classroomRepository,
        UnitOfWork unitOfWork)
    {
        _repository = repository;
        _studentRepository = studentRepository;
        _classroomRepository = classroomRepository;
        _unitOfWork = unitOfWork;
        _professorRepository = professorRepository;
    }
    
    public async Task<int> Add(AddStudentClassroomDto dto)
    {
        await StopIfStudentNotFound(dto);
        
        var classroom =  await StopIfClassroomNotFound(dto);
        
        await StopIfClassroomCompletedCapacity(dto, classroom.Capacity);
        
        await StopIfClassroomTimeInterference(dto, classroom);
        
        var personClassroom = PrototypingOfStudentClassroom(dto);
        
        await _repository.Add(personClassroom);
        
        await _unitOfWork.Complete();

        return personClassroom.Id;
    }

    public async Task Delete(int id)
    {
        var personClassroom = await StopIfStudentClassroomNotFound(id);
        
        _repository.Delete(personClassroom);
        
        await _unitOfWork.Complete();
    }
    
    private async Task StopIfStudentNotFound(AddStudentClassroomDto dto)
    {
        var student = await _studentRepository.IsExistStudent(dto.StudentId);
        if (!student)
        {
            throw new StudentNotFoundException();
        }
    }
    
    private async Task<StudentClassroom> StopIfStudentClassroomNotFound(int id)
    {
        var studentClassroom = await _repository.FindById(id);
        if (studentClassroom == null)
        {
            throw new StudentClassroomNotFoundException();
        }

        return studentClassroom;
    }
    
    private async Task<Classroom> StopIfClassroomNotFound(AddStudentClassroomDto dto)
    {
        var classroom = await _classroomRepository.FindById(dto.ClassroomId);
        if (classroom == null)
        {
            throw new ClassroomNotFoundException();
        }

        return classroom;
    }
    
    private async Task StopIfClassroomCompletedCapacity(AddStudentClassroomDto dto, byte capacity)
    {
        var classrooms = await _classroomRepository.FindAllById(dto.ClassroomId);
        if (classrooms.Count >= capacity)
        {
            throw new ClassroomCompletedCapacityException();
        }
    }
    
    private async Task StopIfClassroomTimeInterference(AddStudentClassroomDto dto, Classroom classroom)
    {
        var studentClassrooms = await _classroomRepository.FindAllClassroomByStudentId(
            dto.StudentId, classroom.Id);
        foreach (var studentClassroom in studentClassrooms)
        {
            if ((classroom.StartDate >= studentClassroom.StartDate && classroom.StartDate <= studentClassroom.EndDate) ||
                (classroom.EndDate >= studentClassroom.StartDate && classroom.EndDate <= studentClassroom.EndDate))
            {
                throw new ClassroomTimeInterferenceException();
            }
        }
    }
    
    private static StudentClassroom PrototypingOfStudentClassroom(AddStudentClassroomDto dto)
    {
        var studentClassroom = new StudentClassroom
        {
            StudentId = dto.StudentId,
            ClassroomId = dto.ClassroomId
        };
        return studentClassroom;
    }
}