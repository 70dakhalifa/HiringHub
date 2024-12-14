using CV.Filtation.System.API.DTO;
using CV_Filtation_System.Core.Entities;
using CV_Filtation_System.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CV.Filtation.System.API.Controllers
{
    public class CompanyController : APIBaseController
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public CompanyController(ICompanyRepository companyRepository,
            IPasswordHasher passwordHasher,
            IConfiguration configuration)
        {
            _companyRepository = companyRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<CompanyDTO>> Register(RegisterCompanyDTO model)
        {
            if (ModelState.IsValid)
            {
                string passwordHash = _passwordHasher.Hash(model.Password);
                var company = new Company
                {
                    Name = model.Name,
                    Email = model.Email,
                    Description = model.Description,
                    Location = model.Location,
                    Website = model.Website,
                    Password = passwordHash
                };

                var result = await _companyRepository.AddAsync(company);

                if (result != null)
                {
                    var token = GenerateToken(company);

                    var returnedCompany = new CompanyDTO
                    {
                        Email = company.Email,
                        Token = token
                    };

                    return Ok(new
                    {
                        Message = "Company registered successfully",
                        Company = returnedCompany
                    });
                }
            }
            return BadRequest("Invalid registration data");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<CompanyDTO>> Login(LoginDTO model)
        {
            var company = await _companyRepository.GetByEmailAsync(model.Email);

            if (company == null)
            {
                return BadRequest("Invalid email or password");
            }

            var isPasswordValid = _passwordHasher.Verify(company.Password, model.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Invalid email or password");
            }

            var token = GenerateToken(company);

            var returnedCompany = new CompanyDTO
            {
                Email = company.Email,
                Token = token
            };

            return Ok(new
            {
                Message = "Login successful",
                Company = returnedCompany
            });
        }

        private string GenerateToken(Company company)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, company.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, company.Name)
        };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
