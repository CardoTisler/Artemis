using Common.Models;
using IdentityApi.Dto;
using IdentityApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers;

[Route("identity")]
[ApiController]
public class UserController(
    IUserRepository repository,
    PasswordHasher.IPasswordHasher passwordHasher,
    TokenGenerator tokenGenerator)
    : Controller
{
    [HttpPost("login", Name = "Login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Login(string email, string password)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (!repository.GetUserExistsByEmail(email)) return NotFound();
        
        var user = repository.GetUserByEmail(email);
        
        if (user == null) return NotFound();

        if (!passwordHasher.VerifyHashedPassword(user.Password, password))
            return Unauthorized("password is incorrect.");
        
        var accessToken = tokenGenerator.GenerateToken(email, user.Id);
            
        return Ok(new LoginResponse { AccessToken = accessToken});
    }

    [HttpPost("register", Name = "Register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Register(string email, string password)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (repository.GetUserExistsByEmail(email)) return Unauthorized("Email is already registered.");

        var hashedPassword = passwordHasher.HashPassword(password);
        var newUser = new User{ Email = email, Password = hashedPassword };
        
        var added = repository.AddUser(newUser);
        
        if (added) return NoContent();
        return BadRequest();
    }
}