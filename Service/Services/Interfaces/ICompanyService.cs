using Service.DTOs.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICompanyService
    {
        Task CreateAsync(CompanyCreateDto model);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, CompanyUpdateDto model);
        Task<CompanyDto> GetByIdAsync(int? id);
        Task<List<CompanyDto>> GetAllAsync();
    }
}
