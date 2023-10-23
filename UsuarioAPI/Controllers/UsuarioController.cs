using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[Route("api/users")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private List<User> _users = new List<User>(); // Simulação de uma lista de usuários em memória

    private AppDbContext _appDbContext;
    public UsuarioController( AppDbContext appDbContext)
    {
       _appDbContext= appDbContext;
    }
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
       var usuarios = _appDbContext.Users.ToList();  
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(string id)
    {
        var user = _appDbContext.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    //select * From users where name = 'Anderson'
    [HttpGet("{name}")]
    public ActionResult<List<User>> GetByUser(string name)
    {
        var users = _appDbContext.Users.Where(x => x.FirstName == name).ToList();
        if (users == null)
            return NotFound();

        return Ok(users);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        user.Id = string.Empty;
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(string id, User updatedUser)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
            return NotFound();

        existingUser.FirstName = updatedUser.FirstName;
        existingUser.LastName = updatedUser.LastName;
        existingUser.Age = updatedUser.Age;
        existingUser.Sex = updatedUser.Sex;
        existingUser.Email = updatedUser.Email;
        existingUser.Password = updatedUser.Password;
        existingUser.Telephone = updatedUser.Telephone;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        _users.Remove(user);
        return NoContent();
    }
}
