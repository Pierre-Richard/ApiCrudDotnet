using System;
using CrudWebApi.Models;


namespace CrudWebApi.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUserById(int id);
        User CreateUser(User user);
        User UpdateUser(int id, User user);
        void DeleteUser(int id);
    }
}
