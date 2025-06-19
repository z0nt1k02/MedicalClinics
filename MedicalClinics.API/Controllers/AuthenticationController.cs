using System.Security.Authentication;
using MedicalClinics.Application.DTOs.Auth;
using MedicalClinics.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalClinics.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationDto registrationDto)
    {
        try
        {
            await _authenticationService.Registration(registrationDto.login, registrationDto.password);
            return Ok("Registration successful");
        }
        catch (AuthenticationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        HttpContext context = HttpContext;
        try
        {
            var token = await _authenticationService.Login(loginDto.login, loginDto.password);
            context.Response.Cookies.Append("token", token);
            return Ok("Login successful");
        }
        catch (AuthenticationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}