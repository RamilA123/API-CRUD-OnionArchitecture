using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Company;
using Service.DTOs.Employee;
using Service.Services;
using Service.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Api_OnionArchitecture_Crud.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _employeeService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("{searchText}")]
        public async Task<IActionResult> GetBySearchText([FromRoute] string searchText)
        {
            var employees = await _employeeService.GetAllAsync();

            return Ok(employees.Where(m => m.Fullname.ToLower().Trim().Contains(searchText.ToLower().Trim())));
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([Required] int id)
        {
            await _employeeService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _employeeService.CreateAsync(request);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EmployeeUpdateDto request)
        {
            await _employeeService.UpdateAsync(id, request);

            return Ok();
        }
    }
}
