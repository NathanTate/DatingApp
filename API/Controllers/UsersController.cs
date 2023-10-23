using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<AppUser>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }

    [AllowAnonymous]
    [HttpGet("search")]

    public async Task<IEnumerable<AppUser>> SearchUser(string userName)
    {
        IEnumerable<AppUser> users = await _context.Users.ToListAsync();

        if (!string.IsNullOrEmpty(userName))
        {
            users = users.Where(u => u.UserName.Contains(userName));
        }

        return users;
    }


    [HttpGet("{id}")] // /api/users/2

    public async Task<AppUser> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user;
    }
}
