using AutoMapper;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Interfaces.Repository;
using CleanArchMediatR.Template.Infra.Data;
using CleanArchMediatR.Template.Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMediatR.Template.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> AddAsync(User entity)
        {
            UserEntity dataEntity = _mapper.Map<UserEntity>(entity);
            _context.Add(dataEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<User>(dataEntity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (Guid.TryParse(id, out Guid userId)) {
                UserEntity? userEntity = await _context.USERS.FindAsync(new object[] { userId });

                if (userEntity is null) return false;

                _context.USERS.Remove(userEntity);
                int effectRow = await _context.SaveChangesAsync();
                return effectRow > 0;
            }
            else { 
                return false;
            }
                
        }

        public async Task<User[]> GetAllAsync()
        {
            List<UserEntity> dataEntities = await _context.USERS.ToListAsync();
            return _mapper.Map<User[]>(dataEntities);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            if (Guid.TryParse(id, out Guid userId))
            {
                UserEntity? dataEntity = await _context.USERS.FindAsync(new object[] { userId });

                return dataEntity == null ? new User() : _mapper.Map<User>(dataEntity);
            }
            else {
                return new User();
            }
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            UserEntity? dataEntity = await _context.USERS.FirstOrDefaultAsync(x => x.UserName.Equals(userName));
            return dataEntity == null ? new User() : _mapper.Map<User>(dataEntity);
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            UserEntity? existingEntity = await _context.USERS.FindAsync(new object[] { entity.id });
            if (existingEntity == null) return false;

            _mapper.Map(entity, existingEntity);
            int effectRow = await _context.SaveChangesAsync();
            return effectRow > 0;
        }
    }
}
