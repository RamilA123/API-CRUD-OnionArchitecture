using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.DTOs.Company;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;

        public CompanyService(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(CompanyCreateDto model)
        {
            Company company = new()
            {
                Name = model.Name,
            };

            await _repo.CreateAsync(company);
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var company = await _repo.GetByIdAsync(id);

            await _repo.DeleteAsync(company);
        }

        public async Task<List<CompanyDto>> GetAllAsync()
        {
            var companies = await _repo.GetAllAsync();
            List<CompanyDto> mappedCompanies = new();

            foreach (var item in companies)
            {
                mappedCompanies.Add(new CompanyDto
                {
                    Name = item.Name,
                });
            }

            return mappedCompanies;
        }

        public async Task<CompanyDto> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var company = await _repo.GetByIdAsync(id);

            CompanyDto mappedCompany = new()
            {
                Name = company.Name,
            };

            return mappedCompany;
        }

        public async Task UpdateAsync(int? id, CompanyUpdateDto model)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var company = await _repo.GetByIdAsync(id);

            company.Name = model.Name;

            await _repo.UpdateAsync(company);
        }
    }
}
