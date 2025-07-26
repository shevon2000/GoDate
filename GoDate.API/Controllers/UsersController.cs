using System.Security.Claims;
using AutoMapper;
using GoDate.API.Entities.DTO;
using GoDate.API.Repositories.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoDate.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await repository.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await repository.GetUserAsync(username);

            if (user == null) return NotFound();

            return user;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username == null) return BadRequest("No username found in token");

            var user = await repository.GetByNameAsync(username);

            if (user == null) return BadRequest("Could not find user");

            mapper.Map(memberUpdateDto, user);

            if (await repository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update the user");

        }
    }
}
