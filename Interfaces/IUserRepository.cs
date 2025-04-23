using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectSync_back.Dtos.User;
using projectSync_back.Models;

namespace projectSync_back.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task <User?> GetByIdAsync(int id);
        Task <User> CreateAsync(User userModel);
        Task<User> UpdateAsync(int id,UpdateUserRequestDto updateUser);
        Task<User>DeleteAsync(int id);
        Task<bool> AssignUserToProjectAsync(int userId, int projectId);
        Task<bool> RemoveUserFromProjectAsync(int userId, int projectId);
        Task<IEnumerable<User>> GetUsersByProjectIdAsync(int projectId);
        Task<bool> IsUserAssignedToProjectAsync(int userId, int projectId);


        

       
    }
}