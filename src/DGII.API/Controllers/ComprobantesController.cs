using DGII.Application.DTOs.Comprobantes;
using DGII.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DGII.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComprobantesController : ControllerBase
{
    private readonly IComprobanteService _service;

    public ComprobantesController(IComprobanteService service)
    {
        _service = service;
    }

    [HttpGet("contribuyente/{rncCedula}")]
    public async Task<IActionResult> GetByRncCedula(string rncCedula, CancellationToken ct)
    {
        var result = await _service.GetByRncCedulaAsync(rncCedula, ct);
        return Ok(result);
    }

    [HttpGet("contribuyente/{rncCedula}/total-itbis")]
    public async Task<IActionResult> GetTotalItbisByRncCedula(string rncCedula, CancellationToken ct)
    {
        var total = await _service.GetTotalItbisByRncCedulaAsync(rncCedula, ct);
        return Ok(new { totalItbis = total });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ComprobanteRequestDto request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return Created(string.Empty, result);
    }
}
