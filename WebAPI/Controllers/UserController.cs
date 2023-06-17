using Microsoft.AspNetCore.Mvc;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Service.Interface;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("all")]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }


    [HttpPost("add")]
    public IActionResult AddUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        if (_userService.AddUser(user))
        {
            return Ok();
        }
        else
        {
            return StatusCode(500, "Failed to add user");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromQuery] int id, [FromBody] User user)
    {
        if (user == null || user.Id != id)
        {
            return BadRequest();
        }
        bool updated = _userService.UpdateUser(user);
        if (updated)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }

    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteUser(int id)
        {
        if (id == 0)
        {
            return BadRequest();
        }

        var deletedUser = _userService.DeleteUser(id);
        if (deletedUser == null)
        {
            return NotFound();
        }

        return Ok();
    }

}