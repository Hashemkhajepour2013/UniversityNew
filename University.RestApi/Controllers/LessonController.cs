using Microsoft.AspNetCore.Mvc;
using University.Services.Lessons.Contracts;
using University.Services.Lessons.Contracts.Dtos;

namespace University.RestApi.Controllers;

public sealed class LessonController : Controller
{
    private readonly LessonService _service;

    public LessonController(LessonService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<int> Add(AddLessonDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpPut("{id}")]
    public async Task Edit(UpdateLessonDto dto, int id)
    {
        await _service.Update(dto, id);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}
