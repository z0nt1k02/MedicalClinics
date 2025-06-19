using System.ComponentModel.DataAnnotations;
using MedicalClinics.Application.DTOs;
using MedicalClinics.Application.DTOs.FreeRecords;
using MedicalClinics.Application.Interfaces;
using MedicalClinics.Application.Mapping;
using MedicalClinics.Core.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalClinics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CabinetController : ControllerBase
{
    private readonly ICabinetService _cabinetService;
    private readonly IFreeRecordService _freeRecordService;

    public CabinetController(ICabinetService cabinetService,IFreeRecordService freeRecordService)
    {
        _cabinetService = cabinetService;
        _freeRecordService = freeRecordService;
    }

    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCabinetWithFreeDays(Guid id,[FromQuery] int? year, [FromQuery] int? month)
    {
        try
        {
            CabinetEntity cabinet = await _cabinetService.GetCabinetWithFreeDaysByIdAsync(id,month, year);
            return Ok(cabinet.ToDtoDays());
        }
        catch (NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [Authorize]
    [HttpGet]
    [Route("{id}/date")]
    public async Task<IActionResult> GetCabinetWithFreeHours(Guid id,[FromQuery,Required(ErrorMessage = "Year is required")] int year, 
        [FromQuery,Required(ErrorMessage = "Month is required")] int month,
        [FromQuery,Required(ErrorMessage = "Day is required")]int day)
    {
        try
        {
            CabinetEntity cabinet = await _cabinetService.GetCabinetWithFreeHoursByIdAsync(id,day,month, year);
            return Ok(cabinet.ToDtoHours());
        }
        catch (NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [Authorize(policy: "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCabinet(CreateCabinetDto cabinetDto)
    {
        CabinetEntity newCabinet = await _cabinetService.CreateCabinetAsync(cabinetDto);
        return CreatedAtAction
        (
            nameof(GetCabinetWithFreeDays),
            new {id=newCabinet.Id},
            newCabinet.ToDtoDays()
        );
    }
    
    [Authorize(policy: "Admin")]
    [HttpPost]
    [Route("{cabinetId}/addfreerecord")]
    public async Task<IActionResult> AddFreeRecord(CreateFreeRecordDto dto, Guid cabinetId)
    {
        FreeRecordEntity freeRecordEntity = await _freeRecordService.AddFreeRecord(dto, cabinetId);
        return Ok(freeRecordEntity.ToDto());
    }

    [Authorize(policy: "Admin")]
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