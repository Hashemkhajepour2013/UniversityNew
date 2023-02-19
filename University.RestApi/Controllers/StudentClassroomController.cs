using Microsoft.AspNetCore.Mvc;
using University.Services.StudentClassrooms.Contracts;
using University.Services.StudentClassrooms.Contracts.Dtos;

namespace University.RestApi.Controllers;

public sealed class StudentClassroomController : Controller
{
    private readonly StudentClassroomService _service;

    public StudentClassroomController(StudentClassroomService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<int> Add(AddStudentClassroomDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}