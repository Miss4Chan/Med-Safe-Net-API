using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Med_Safe_Net_API.DTOs;
using Med_Safe_Net_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            Username = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key,
            DateOfBirth = registerDto.DateOfBirth,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email
        };
        context.Users.Add(user);

        await context.SaveChangesAsync();

        var userRole = new UserRole
        {
            Id = user.Id,
            AppRoleId = AppRoleType.Caregiver
        };

        context.UserRoles.Add(userRole);

        await context.SaveChangesAsync();

        return Ok(new UserDto
        {
            Username = user.Username,
            Token = tokenService.CreateToken(user)
        });
    }

    [Authorize(Roles = nameof(AppRoleType.Administrator))]
    [HttpPost("register-patient")]
    public async Task<ActionResult<PatientDto>> RegisterPatient(RegisterDto registerDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        using var hmac = new HMACSHA512();

        var patient = new AppUser
        {
            Username = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key,
            DateOfBirth = registerDto.DateOfBirth,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email
        };
        context.Users.Add(patient);
        await context.SaveChangesAsync();

        var userRole = new UserRole
        {
            Id = patient.Id,
            AppRoleId = AppRoleType.Patient
        };
        context.UserRoles.Add(userRole);

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

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users
            .Include(e => e.UserRoles)
            .FirstOrDefaultAsync(x =>
         x.Username == loginDto.Username.ToLower());


        if (user == null && user?.Username == null) return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        return new UserDto
        {
            Username = user.Username!,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.Username!.ToLower() == username.ToLower());
    }
}