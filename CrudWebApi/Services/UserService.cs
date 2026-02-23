using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using CrudWebApi.Data;
using CrudWebApi.Models;
using CrudWebApi.Repositories;
using CrudWebApi.Services;

public class UserService : IUserService
{
    // j'inject mon IUserRepository dans mon construteur afin d'utilise c'est methode dans ma class UserService
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    

    public User CreateUser(User user)
    {
        return _userRepository.CreateUser(user);
    }

    public void DeleteUser(int id)

    {

        _userRepository.DeleteUser(id);
        
    }

    public User GetUserById(int id)

    {
        return _userRepository.GetUserById(id); 
       
    }

    public List<User> GetUsers()
    {
        return _userRepository.GetUsers();
    }

    public User UpdateUser(int id, User user)
    {
        return _userRepository.UpdateUser(id, user);
    }
}