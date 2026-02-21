using System;
using CrudWebApi.Models;
namespace CrudWebApi.Repositories
{
    public interface IUserRepository
    {
        
        List<User> GetUsers();
        User GetUserById(int id);
        User CreateUser(User user);
        User UpdateUser(int id, User user);
        void DeleteUser(int id);
    }
}
