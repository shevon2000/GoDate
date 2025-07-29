using System.Security.Claims;
using AutoMapper;
using GoDate.API.Entities.Domain;
using GoDate.API.Entities.DTO;
using GoDate.API.Extensions;
using GoDate.API.Repositories.UserRepo;
using GoDate.API.Services.PhotoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoDate.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;
        private readonly IPhotoService service;

        public UsersController(IUserRepository repository, IMapper mapper, IPhotoService service)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.service = service;
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

            var user = await repository.GetByNameAsync(User.GetUsername());

            if (user == null) return BadRequest("Could not find user");

            mapper.Map(memberUpdateDto, user);

            if (await repository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update the user");

        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await repository.GetByNameAsync(User.GetUsername());

            if (user == null) return BadRequest("Cannot update user");

            var result = await service.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            user.Photos.Add(photo);

            if (await repository.SaveAllAsync())
                return CreatedAtAction(nameof(GetUser),                             // return location of the created photo
                    new { username = user.UserName }, mapper.Map<PhotoDto>(photo)); // eg:- url/api/users/erica

            return BadRequest("Problem adding photo");

        }

        [HttpPut("set-main-photo/{photoId:int}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await repository.GetByNameAsync(User.GetUsername());

            if (user == null) return BadRequest("Could not find user");

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null || photo.IsMain) return BadRequest("This cannot used as the main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await repository.SaveAllAsync()) return NoContent();

            return BadRequest("There is a problem in setting main photo");
        }

        [HttpDelete("delete-photo/{photoId:int}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await repository.GetByNameAsync(User.GetUsername());

            if (user == null) return BadRequest("User not found");

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null || photo.IsMain) return BadRequest("This photo cannot be deleted");

            if (photo.PublicId != null)
            {
                var result = await service.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await repository.SaveAllAsync()) return Ok();

            return BadRequest("Problem in deleting photo");

        }
        


    }
}
