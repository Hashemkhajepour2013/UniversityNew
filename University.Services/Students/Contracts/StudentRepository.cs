using University.Entities;

namespace University.Services.Students.Contracts;

public interface StudentRepository
{
    Task Add(Student student);
    
    Task<bool> MobileIsExist(string mobile);

    Task<Student?> FindById(int id);

    void Delete(Student student);

    Task<bool> IsExistStudent(int id);
}