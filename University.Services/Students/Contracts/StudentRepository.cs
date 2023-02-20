using University.Entities;
using University.Services.Infrastructure;

namespace University.Services.Students.Contracts;

public interface StudentRepository : Service
{
    Task Add(Student student);
    
    Task<bool> MobileIsExist(string mobile);

    Task<Student?> FindById(int id);

    void Delete(Student student);

    Task<bool> IsExistStudent(int id);
}