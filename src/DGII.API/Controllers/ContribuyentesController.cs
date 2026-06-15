using DGII.Application.DTOs.Contribuyentes;
using DGII.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DGII.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContribuyentesController : ControllerBase
{
    private readonly IContribuyenteService _service;

    public ContribuyentesController(IContribuyenteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _service.GetAllAsync(ct);
        return Ok(result);
    }

    [HttpGet("{rncCedula}")]
    public async Task<IActionResult> GetByRncCedula(string rncCedula, CancellationToken ct)
    {
        var result = await _service.GetByRncCedula(rncCedula, ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ContribuyenteRequestDto request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetByRncCedula), new { rncCedula = result.RncCedula }, result);
    }
}
