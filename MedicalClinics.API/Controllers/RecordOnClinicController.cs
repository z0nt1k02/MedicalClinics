using System.Security.Claims;
using MedicalClinics.Application.DTOs.RecordOnClinic;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Application.Mapping;
using MedicalClinics.Core.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalClinics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordOnClinicController : ControllerBase
{
    private readonly IRecordOnClinicService _recordOnClinicService;

    public RecordOnClinicController(IRecordOnClinicService recordOnClinicService)
    {
        _recordOnClinicService = recordOnClinicService;
    }

    [Authorize(policy: "Admin")]
    [Route("admin")]
    [HttpGet]
    public async Task<IActionResult> RecordsOnClinicAdmin( [FromQuery] Guid? cabinetId,[FromQuery] int? clinicId)
    {
        List<RecordOnClinicEntity> recordsOnClinic = await _recordOnClinicService.GetRecordsAdmin(cabinetId, clinicId);
        return Ok(recordsOnClinic.Select(r=>r.ToDto()).ToList());
    }

    [Authorize(policy: "User")]
    [HttpPost]
    public async Task<IActionResult> CreateRecordOnClinic(CreateRecordOnClinicDto dto)
    {
        HttpContext context = HttpContext;
        RecordOnClinicEntity recordOnClinic = await _recordOnClinicService.
            AddRecordOnClinic(dto, Guid.Parse(HttpContext!.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value));
        
        return Ok("Record on clinic successful added");
    }

    [Authorize(policy: "User")]
    [HttpGet]
    public async Task<IActionResult> GetRecordsOnClinic()
    {
        HttpContext context = HttpContext;
        List<RecordOnClinicEntity> records =await _recordOnClinicService
            .GetRecords(
                Guid.Parse
                    (context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value));
        return Ok(records.Select(r => r.ToDto()).ToList());
    }
    
}