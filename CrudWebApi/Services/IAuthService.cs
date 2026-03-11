using System;
using CrudWebApi.DTOs;


namespace CrudWebApi.Services
{
    public interface IAuthService
    {
        AuthResponseDto Register(RegisterDto user);
        AuthResponseDto Login(LoginDto user);
    }
}
        