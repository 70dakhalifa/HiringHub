using CV_Filtation_System.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Filtation_System.Core.Repositories
{
    public interface ICompanyRepository
    {
        Task<IReadOnlyList<Company>> GetAllAsync();
        Task<Company> GetByEmailAsync(string email);
        Task<Company> AddAsync(Company item);
        Task DeleteAsync(Company item);
        Task UpdateAsync(Company item);

    }
}
