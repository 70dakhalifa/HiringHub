using CV_Filtation_System.Core.Entities;
using CV_Filtation_System.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public async Task UpdateAsync(Company item)
        {
            _context.Companies.Update(item);
            await _context.SaveChangesAsync();
        }
        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Company> AddAsync(Company item)
        {
            _context.Companies.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public Task DeleteAsync(Company item)
        {
            throw new NotImplementedException();
        }
        public Task<IReadOnlyList<Company>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Company> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            var re = await _context.Companies.FirstOrDefaultAsync(c => c.Email == email);
            
            return re;
        }

    }

}
