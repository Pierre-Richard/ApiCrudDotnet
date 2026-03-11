using System;
using CrudWebApi.DTOs;
using CrudWebApi.Models;
using CrudWebApi.Services;
using Microsoft.AspNetCore.Mvc;



namespace CrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase

    {
        private IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // post
        [HttpPost("register")]
        public ActionResult<AuthResponseDto> Register([FromBody] RegisterDto user) => Ok(_authService.Register(user));

        //Get
         [HttpPost("login")]
        public ActionResult<AuthResponseDto> Login([FromBody] LoginDto user) => Ok(_authService.Login(user));

    }
    

}
