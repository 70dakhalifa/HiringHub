using CV.Filtation.System.API.DTO;
using CV_Filtation_System.Core.Entities;
using CV_Filtation_System.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CV.Filtation.System.API.Controllers
{
    public class CompanyController : APIBaseController
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IPasswordHasher _passwordHasher;


        public CompanyController(ICompanyRepository companyRepository, 
            IPasswordHasher passwordHasher)
        {
            _companyRepository = companyRepository;
            _passwordHasher = passwordHasher;
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
                var ch = await _companyRepository.AddAsync(company);
                return Ok(new{Message = "Your registered successfully"});
            }
            return BadRequest();
        }
        [HttpPost("Login")]
        public async Task<ActionResult<CompanyDTO>> Login(LoginDTO model)
        {
            var company = await _companyRepository.GetByEmailAsync(model.Email);
            var result = _passwordHasher.Verify(company.Password, model.Password);
            if (!result)
            {
                return BadRequest("Email Or Password is not correct");
            }
            var returnedCompany = new CompanyDTO
            {
                Email = company.Email,
                Token = "This is Token"
            };
            return Ok(new { Message = "Your login successfully" });
        }
    }
}
