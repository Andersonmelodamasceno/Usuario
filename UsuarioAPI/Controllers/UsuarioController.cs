using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[Route("api/users")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private List<User> _users = new List<User>(); // Simulação de uma lista de usuários em memória

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(_users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        user.Id = Guid.NewGuid();
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, User updatedUser)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
            return NotFound();

        existingUser.Name = updatedUser.Name;
        existingUser.LastName = updatedUser.LastName;
        existingUser.Age = updatedUser.Age;
        existingUser.Sex = updatedUser.Sex;
        existingUser.Email = updatedUser.Email;
        existingUser.Password = updatedUser.Password;
        existingUser.Telephone = updatedUser.Telephone;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        _users.Remove(user);
        return NoContent();
    }
}
