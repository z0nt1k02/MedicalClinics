using MedicalClinics.Application.DTOs;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Application.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalClinics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicController : ControllerBase
{
    private readonly IClinicService _clinicService;

    public ClinicController(IClinicService clinicService)
    {
        _clinicService = clinicService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllClinics()
    {
        var clinics = await _clinicService.GetAllClinicsAsync();
        return Ok(clinics.Select(c => c.ToDto()).ToList());
    }

    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetClinicById(int id)
    {
        try
        {
            var clinic = await _clinicService.GetClinicByIdAsync(id);
            return Ok(clinic.ToDto());
        }
        catch (NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [Authorize(policy: "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateClinic(CreateClinicDto dto)
    {
        var newClinic = await _clinicService.CreateClinicAsync(dto);
        return CreatedAtAction
        (
            nameof(GetClinicById),
            new {id=newClinic.Id},
            newClinic.ToDto()
        );
        
    }
    
    [Authorize(policy: "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteClinic(int id)
    {
        try
        {
            await _clinicService.DeleteClinicAsync(id);
            return Ok("Clinic deleted");
        }
        catch(NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    
    
}