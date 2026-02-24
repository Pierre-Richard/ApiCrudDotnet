using System;
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
        public ActionResult<string> Register([FromBody] User user) => Ok(_authService.Register(user));

        //Get
         [HttpGet("login")]
           public ActionResult<string> Login([FromBody] User user) => Ok(_authService.Login(user));

    }
    

}
