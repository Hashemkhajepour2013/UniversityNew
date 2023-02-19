using Microsoft.AspNetCore.Mvc;
using University.Services.Classrooms.Contracts;
using University.Services.Classrooms.Contracts.Dtos;

namespace University.RestApi.Controllers;

public sealed class ClassroomController : Controller
{
    private readonly ClassroomService _service;

    public ClassroomController(ClassroomService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<int> Add(AddClassroomDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpPut("{id}")]
    public async Task Edit(UpdateClassroomDto dto, int id)
    {
        await _service.Update(dto, id);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}