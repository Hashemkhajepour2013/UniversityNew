using Microsoft.AspNetCore.Mvc;
using University.Services.Terms.Contracts;
using University.Services.Terms.Contracts.Dtos;

namespace University.RestApi.Controllers;

public sealed class TermController : Controller
{
    private readonly TermService _service;

    public TermController(TermService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<int> Add(AddTermDto dto)
    {
        return await _service.Add(dto);
    }

    [HttpPut("{id}")]
    public async Task Edit(UpdateTermDto dto, int id)
    {
        await _service.Update(dto, id);
    }

    [HttpDelete("id")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}