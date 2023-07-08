using Service.DTOs.Employee;

namespace Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task CreateAsync(EmployeeCreateDto model);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, EmployeeUpdateDto model);
        Task<EmployeeDto> GetByIdAsync(int? id);
        Task<List<EmployeeDto>> GetAllAsync();
    }
}
