using University.Entities;
using University.Services.Documents.Exceptions;
using University.Services.Students.Contracts;
using University.Services.Students.Contracts.Dtos;
using University.Services.Students.Extensions;

namespace University.Services.Students;

public sealed class StudentAppService : StudentService
{
    private readonly StudentRepository _repository;
    private readonly UnitOfWork _unitOfWork;

    public StudentAppService(
        StudentRepository repository,
        UnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Add(AddStudentDto dto)
    {
        await StopIfMobileIsExist(dto);
        
        var student = PrototypingOfStudent(dto); 
        
        await _repository.Add(student);
        
        await _unitOfWork.Complete();
        
        return student.Id;
    }

    public async Task Update(UpdateStudentDto dto, int id)
    {
        var student = await StopIfStudentNotFound(id);
        
        EditStudent(dto, student);
        
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var student = await StopIfStudentNotFound(id);
        _repository.Delete(student);
        await _unitOfWork.Complete();
    }
    
    private static Student PrototypingOfStudent(AddStudentDto dto)
    {
        var student = new Student
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Mobile = dto.Mobile,
            StudentNumber = dto.StudentNumber
        };
        return student;
    }
    
    private async Task StopIfMobileIsExist(AddStudentDto dto)
    {
        var mobileIsExist = await _repository.MobileIsExist(dto.Mobile);
        if (mobileIsExist)
        {
            throw new MobileIsDuplicateException();
        }
    }
    
    private async Task<Student> StopIfStudentNotFound(int id)
    {
        var student = await _repository.FindById(id);
        if (student == null)
        {
            throw new StudentNotFoundException();
        }

        return student;
    }
    
    private static void EditStudent(UpdateStudentDto dto, Student student)
    {
        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.StudentNumber = dto.StudentNumber;
    }
}