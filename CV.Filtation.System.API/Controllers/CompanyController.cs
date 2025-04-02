using CV.Filtation.System.API.DTO;
using CV.Filtation.System.API.DTO.Company;
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
                    int id = result.CompanyId;
                    string Email = company.Email;

                    return Ok(new
                    {
                        Message = "Company registered successfully",
                        Id = id,
                        Email = Email
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
                Id = company.CompanyId,
                Token = token
            };

            return Ok(new
            {
                Message = "Login successful",
                Company = returnedCompany
            });
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetAllCompanies()
        {
            var companies = await _companyRepository.GetAllAsync();
            var companyDTOs = companies.Select(c => new Company
            {
                CompanyId = c.CompanyId,
                Name = c.Name,
                Email = c.Email,
                Description = c.Description,
                Location = c.Location,
                Website = c.Website,
                ProfilePicture = c.ProfilePicture
            }).ToList();

            return Ok(companyDTOs);
        }
        [HttpGet("GetCompanyProfile")]
        public async Task<ActionResult<CompanyDTO>> GetCompanyByEmail(string email)
        {
            var company = await _companyRepository.GetByEmailAsync(email);
            if (company == null)
            {
                return NotFound("Company not found.");
            }

            var companyDTO = new CompanyProfileDTO
            {
                Name = company.Name,
                Email = company.Email,
                Description = company.Description,
                Location = company.Location,
                Website = company.Website,
                ProfilePicture = company.ProfilePicture
            };

            return Ok(companyDTO);
        }
        [HttpPost("UploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture(string Email, IFormFile file)
        {
            var company = await _companyRepository.GetByEmailAsync(Email);
            if (company == null)
            {
                return NotFound("Company not found");
            }

            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Company_profile_pictures");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Save new profile picture
            string fileName = $"{Email}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            company.ProfilePicture = "/Company_profile_pictures/" + fileName;
            await _companyRepository.UpdateAsync(company);

            return Ok(new { Message = "Profile picture uploaded successfully", FileName = company.ProfilePicture });
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
