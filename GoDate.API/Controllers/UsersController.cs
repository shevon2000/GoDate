using GoDate.API.Data;
using GoDate.API.Entities.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoDate.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly ApplicationDbContext context;

        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await context.Users.ToListAsync();

            return users;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return user;
        }

    }
}
