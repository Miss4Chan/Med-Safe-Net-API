using System;
using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using Med_Safe_Net_API.DTOs;
using Med_Safe_Net_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController
{

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users
            .Include(e => e.UserRoles)
            .Include(e => e.UserLinks)
            .ToListAsync();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await context.Users
            .Include(u => u.HighHeartRates)
            .Include(u => u.SuddenMovements)
            .Include(u => u.HeartRates
                .Where(hr => hr.Timestamp > DateTime.UtcNow.AddHours(-2)))
            .FirstOrDefaultAsync(e => e.Id == id);
        if (user==null) return NotFound();
        return Ok(user);
    }

    [Authorize(Roles = $"{nameof(AppRoleType.Administrator)},{nameof(AppRoleType.Caregiver)}")]
    [HttpPost("link-patient")]
    public async Task<ActionResult<PatientDto>> LinkPatientToCaregiver(string userCode)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var patient = await context.Users.FirstOrDefaultAsync(u => u.UserCode == new Guid(userCode));

        if (patient==null) return NotFound();

        var userLink = new UserLink
        {
            CareGiverId = userId,
            PatientId = patient.Id,
        };

        context.UserLinks.Add(userLink);

        await context.SaveChangesAsync();

        return Ok(new PatientDto
        {
            Id = patient.Id,
            UserCode = patient.UserCode,
            Username = patient.Username,
            DateOfBirth = patient.DateOfBirth,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Email = patient.Email,
            UserRoles = patient.UserRoles,
            UserLinks = patient.UserLinks
        });

    }

    [Authorize(Roles = $"{nameof(AppRoleType.Administrator)},{nameof(AppRoleType.Caregiver)}")]
    [HttpGet("get-patients")]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetMyPatients()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var patients = await context.Users.Include(u => u.UserLinks)
            .Where(u => context.UserLinks.Where(p => p.PatientId == u.Id)
            .Any() && context.UserLinks.Where(p => p.CareGiverId == userId)
            .Any()).ToListAsync();

        List<PatientDto> patientDtos = patients.Select(patient => new PatientDto
        {
            Id = patient.Id,
            UserCode = patient.UserCode,
            Username = patient.Username,
            DateOfBirth = patient.DateOfBirth,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Email = patient.Email,
            UserRoles = patient.UserRoles,
            UserLinks = patient.UserLinks
        }).ToList();

        return Ok(patientDtos);

    }
}