using CV.Filtation.System.API.DTO;
using CV_Filtation_System.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CV.Filtation.System.API.Controllers
{
    public class AccountController : APIBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager
            ,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO model)
        {
            var user = new User
            {
                Name = model.Name,
                Address = model.Address,
                City = model.City,
                Email = model.Email,
                PhoneNumber = model.Phone,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                UserName=model.Name
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            var returnedUser = new UserDTO
            {
                DisplayName = user.Name,
                Email = user.Email,
                Token = "This Will Be Token"
            };
            return Ok(returnedUser);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid email or password");

            return Ok(new UserDTO
            {
                DisplayName = user.Name,
                Email = user.Email,
                Token = "This is Token"
            });
        }
    }
}
