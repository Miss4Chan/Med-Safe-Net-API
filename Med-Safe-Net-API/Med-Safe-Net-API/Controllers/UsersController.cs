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
        return users;
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user==null) return NotFound();
        return user;
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
}