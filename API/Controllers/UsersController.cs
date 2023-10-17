using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] //https://localhost5001/api/users, the [controller] gets rid of the Controller at the end of out class
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<AppUser>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }

    [HttpGet("{id}")] // /api/users/2

    public async Task<AppUser> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user;
    }
}
