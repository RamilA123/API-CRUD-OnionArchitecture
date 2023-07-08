using Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Company;
using Service.DTOs.Employee;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ICompanyService _companyService;
        private readonly IEmployeeRepository _repo;

        public EmployeeService(ICompanyService companyService,
                               IEmployeeRepository repo)
        {
            _companyService = companyService;
            _repo = repo;
        }

        public async Task CreateAsync(EmployeeCreateDto model)
        {
            if (await _companyService.GetByIdAsync(model.CompanyId) == null) throw new NullReferenceException();
            else
            {
                Employee employee = new()
                {
                    Fullname = model.Fullname,
                    Email = model.Email,
                    CompanyId = model.CompanyId,
                };

                await _repo.CreateAsync(employee);
            }
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var employee = await _repo.GetByIdAsync(id);

            await _repo.DeleteAsync(employee);
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>>[] includes =
{
                entity => entity.Include(m=>m.Company),
            };

            var employees = await _repo.GetAllWithIncludesAsync(includes);
            List<EmployeeDto> mappedEmployees = new();

            foreach (var item in employees)
            {
                mappedEmployees.Add(new EmployeeDto
                {
                    CompanyName = item.Company.Name,
                    Email = item.Email,
                    Fullname = item.Fullname
                });
            }

            return mappedEmployees;
        }

        public async Task<EmployeeDto> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var employee = await _repo.GetByIdWithIncludesAsync((int)id, m => m.Company);

            EmployeeDto mappedEmployee = new()
            {
                Fullname = employee.Fullname,
                CompanyName = employee.Company.Name,
                Email = employee.Email,
            };

            return mappedEmployee;
        }

        public async Task UpdateAsync(int? id, EmployeeUpdateDto model)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            if (await _companyService.GetByIdAsync(model.CompanyId) == null) throw new NullReferenceException(nameof(model.CompanyId));

            var employee = await _repo.GetByIdAsync(id);

            employee.Fullname = model.Fullname;
            employee.Email = model.Email;
            employee.CompanyId = model.CompanyId;

            await _repo.UpdateAsync(employee);
        }
    }
}
