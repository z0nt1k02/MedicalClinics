using MedicalClinics.Application.DTOs.FreeRecords;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Application.Mapping;
using MedicalClinics.Core.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MedicalClinics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FreeRecordController : ControllerBase
{
    private readonly IFreeRecordService _freeRecordService;

    public FreeRecordController(IFreeRecordService freeRecordService)
    {
        _freeRecordService = freeRecordService;
    }

    [HttpPost]
    [Route("{cabinetId}")]
    public async Task<IActionResult> AddFreeRecord(CreateFreeRecordDto dto, Guid cabinetId)
    {
        FreeRecord freeRecord = await _freeRecordService.AddFreeRecord(dto, cabinetId);
        return Ok(freeRecord.ToDto());
    }
}