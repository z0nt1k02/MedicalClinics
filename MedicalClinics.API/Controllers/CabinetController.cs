using MedicalClinics.Application.DTOs;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Application.Mapping;
using MedicalClinics.Core.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MedicalClinics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CabinetController : ControllerBase
{
    private readonly ICabinetService _cabinetService;

    public CabinetController(ICabinetService cabinetService)
    {
        _cabinetService = cabinetService;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCabinetById(Guid id)
    {
        try
        {
            CabinetEntity cabinet = await _cabinetService.GetCabinetByIdAsync(id);
            return Ok(cabinet.ToDto());
        }
        catch (NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> AddCabinet(CreateCabinetDto cabinetDto)
    {
        CabinetEntity newCabinet = await _cabinetService.CreateCabinetAsync(cabinetDto);
        return CreatedAtAction
        (
            nameof(GetCabinetById),
            new {id=newCabinet.Id},
            newCabinet.ToDto()
        );
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteCabinet(Guid id)
    {
        try
        {
            await _cabinetService.DeleteCabinetAsync(id);
            return Ok("Cabinet deleted");
        }
        catch(NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
    }
}