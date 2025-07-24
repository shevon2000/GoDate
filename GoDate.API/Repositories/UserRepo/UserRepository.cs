using AutoMapper;
using AutoMapper.QueryableExtensions;
using GoDate.API.Data;
using GoDate.API.Entities.Domain;
using GoDate.API.Entities.DTO;
using Microsoft.EntityFrameworkCore;

namespace GoDate.API.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users
                .Include(x => x.Photos)                     // To get the related data in Photos table
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User?> GetByNameAsync(string username)
        {
            return await context.Users
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<UserDto?> GetUserAsync(string username)
        {
            return await context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await context.Users
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;    // Value > 0, meaning something has changed
        }

        public void Update(User user)   
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}


// Use AutoMapper to project the query result directly into a UserDto, instead of loading a full User entity.