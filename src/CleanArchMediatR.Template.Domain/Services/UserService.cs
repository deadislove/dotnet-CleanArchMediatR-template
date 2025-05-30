using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Domain.Interfaces.Repository;
using CleanArchMediatR.Template.Domain.Interfaces.Services;

namespace CleanArchMediatR.Template.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) { 
            _userRepository = userRepository; 
        }
        public Task<User> AddAsync(User entity)
        {
            return _userRepository.AddAsync(entity);
        }

        public Task<bool> DeleteAsync(string id)
        {
            return _userRepository.DeleteAsync(id);
        }

        public Task<User[]> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(string id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<User> GetByUserNameAsync(string userName)
        {
            return _userRepository.GetByUserNameAsync(userName);
        }

        public Task<bool> UpdateAsync(User entity)
        {
            return _userRepository.UpdateAsync(entity);
        }
    }
}
