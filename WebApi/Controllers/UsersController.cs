using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Text.Json;
using Zxcvbn;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersService;
        private IPasswordValidation _passwordService;
        public UsersController(IUsersServices usersService, IPasswordValidation passwordService)
        {
            _usersService = usersService;
            _passwordService = passwordService;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user=await _usersService.GetUserByID(id);
            if(user!=null)  return Ok(user);
            return NoContent();
        }

        // POST api/<UsersController>
        [HttpPost("login")]
        public async Task<ActionResult<User>> PostLogin([FromBody] LoginUser loginUser)
        {
            User user =await _usersService.Login(loginUser);
            if (user != null)
                return Ok(user);
            return NoContent();
        }

        [HttpPost("signup")]
        public ActionResult<User> PostSignUp([FromBody] User user)
        {
            var result = Zxcvbn.Core.EvaluatePassword(user.Password);

            Console.WriteLine($"Password score: {result.Score}");

            if (result.Score < 2)
            {
                return BadRequest("Password is too weak.");
            }
            _usersService.SignUp(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }




        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(User userToUpdate)
        {
            if (_passwordService.GetPasswordStrength(userToUpdate.Password).Score <= 2)
                return BadRequest("Weak password");
            try
            {
                var result = await _usersService.UpdateUser(userToUpdate);
                if (result!=null)
                    return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return StatusCode(500);

        }

        // DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        [HttpPost("CheckPassword")]
        public int PasswordCheck([FromBody] string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;
        }

    }
}
