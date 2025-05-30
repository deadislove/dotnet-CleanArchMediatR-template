using CleanArchMediatR.Template.Domain.Entities;

namespace CleanArchMediatR.Template.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetByUserNameAsync(string userName);
        Task<User> AddAsync(User entity);
        Task<User[]> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<bool> UpdateAsync(User entity);
        Task<bool> DeleteAsync(string id);
    }
}
