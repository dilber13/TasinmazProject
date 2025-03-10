using System.Collections.Generic;
using System.Threading.Tasks;
using BackendAPI.Dtos;
using BackendAPI.Entities;




namespace BackendAPI.Business.Abstract
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id); // ID ye gore user for delete
        Task<List<User>> GetAllUsersAsync(); //tum user

        Task<User> AddUserAsync(string email, string password, string role);
        Task<User> UpdateUserAsync(int id, UserDTO userDto);
        Task<bool> DeleteUserAsync(int id);


    }
}
