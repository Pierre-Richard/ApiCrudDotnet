using System;
using CrudWebApi.Models;

namespace CrudWebApi.Services
{
    public interface IAuthService
    {
        string Register(User user);
        string Login(User user);
    }
}
