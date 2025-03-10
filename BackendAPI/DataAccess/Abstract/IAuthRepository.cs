
using BackendAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BackendAPI.DataAccess.Abstract

{ 
    public interface IAuthRepository
    {

        Task<User> Register(User user, string password);
        Task<User> Login(string userEmail, string password);
        Task<bool> UserExists(string userEmail);

    }
}
