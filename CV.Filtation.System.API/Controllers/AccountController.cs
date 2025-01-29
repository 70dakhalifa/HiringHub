using CV.Filtation.System.API.DTO;
using CV_Filtation_System.Core.Entities;
using CV_Filtation_System.Services.Models;
using CV_Filtation_System.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Response = CV_Filtation_System.Services.Models.Response;

namespace CV.Filtation.System.API.Controllers
{
    public class AccountController : APIBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;


        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new { Message = "Password and Confirm Password do not match." });
            }

            var user = new User
            {
                FName = model.FName,
                LName = model.LName,
                Address = model.Address,
                City = model.City,
                Email = model.Email,
                PhoneNumber = model.Phone,
                EmailConfirmed = true,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            //Add Token to Verify the email....
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"User created & Email Sent to {user.Email} SuccessFully" });

            //var token = GenerateToken(user);

            //var returnedUser = new UserDTO
            //{
            //    DisplayName = user.FName,
            //    Email = user.Email,
            //    Token = token
            //};

            //return Ok(new
            //{
            //    Message = "User registered successfully",
            //    User = returnedUser
            //});
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid email or password");

            var token = GenerateToken(user);

            var returnedUser = new UserDTO
            {
                DisplayName = user.FName,
                Email = user.Email,
                Token = token
            };

            return Ok(new
            {
                Message = "User logged in successfully",
                User = returnedUser
            });
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
            
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "This User Doesnot exist!" });
        }
        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var ForgotPasswordLink = Url.Action(nameof(ResetPassword), "Authentication", new {
                    token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot Password link", ForgotPasswordLink!);
                _emailService.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = $"Password Changed request is sent on Email {user.Email}.Please Open your Email & click the link." });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                       new Response { Status = "Error", Message = $"Could not send link to email, Please try again." });
        }
        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new { model });
        }
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!resetPasswordResult.Succeeded)
                {
                    foreach (var error in resetPasswordResult.Errors) // Fixed: Use resetPasswordResult.Errors
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState); // Fixed: Node1State -> ModelState
                }

                return StatusCode(StatusCodes.Status200OK, // Fixed: Status2000K -> Status200OK
                    new Response { Status = "Success", Message = $"Password has been Changed" });
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error", Message = $"Could not send Link to email, Please try again." });
        }
    }
}
    