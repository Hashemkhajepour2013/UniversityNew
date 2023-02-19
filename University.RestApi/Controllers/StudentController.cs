using Microsoft.AspNetCore.Mvc;
using University.Services.Students.Contracts;
using University.Services.Students.Contracts.Dtos;

namespace University.RestApi.Controllers;

public class StudentController : Controller
{
    private readonly StudentService _service;

    public StudentController(StudentService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<int> Add(AddStudentDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpPut("{id}")]
    public async Task Edit(UpdateStudentDto dto, int id)
    {
        await _service.Update(dto, id);
    }

    [HttpDelete("id")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}