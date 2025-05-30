using CleanArchMediatR.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByUserNameAsync(string userName);
        Task<User> AddAsync(User entity);
        Task<User[]> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<bool> UpdateAsync(User entity);
        Task<bool> DeleteAsync(string id);
    }
}
