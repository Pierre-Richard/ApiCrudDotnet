using System;
using CrudWebApi.Models;
using CrudWebApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace CrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        // ici mon class Usercontroller a besoin du IUserService pour faire son travail sur les routes
        private IUserService _uservice;
        //Je passe mon IUserService dans mon construteur
        public UserController(IUserService userService)
        {
            _uservice = userService;
        }

        //Get 
        [HttpGet]
        public ActionResult<List<User>> GetUsers() => Ok(_uservice.GetUsers());
        //Get userById
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById([FromRoute]int id) => Ok(_uservice.GetUserById(id));
        //Post 
        [HttpPost]
        public ActionResult<User> CreteUser([FromBody] User user) => Ok(_uservice.CreateUser(user));
        //Put
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser([FromRoute] int id,[FromBody] User user) => Ok(_uservice.UpdateUser(id, user));

        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)  {
            _uservice.DeleteUser(id);
            return NoContent();
        }
       
         
        
    }
}
