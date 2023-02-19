using Microsoft.AspNetCore.Mvc;
using University.Services.Professors.Contracts;
using University.Services.Professors.Contracts.Dtos;

namespace University.RestApi.Controllers;

public sealed class ProfessorController : Controller
{
    private readonly ProfessorService _service;

    public ProfessorController(ProfessorService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<int> Add(AddProfessorDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpPut("{id}")]
    public async Task Edit(UpdateProfessorDto dto, int id)
    {
        await _service.Update(dto, id);
    }

    [HttpDelete("id")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}