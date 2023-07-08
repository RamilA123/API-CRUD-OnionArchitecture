using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Company;
using Service.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Api_OnionArchitecture_Crud.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _companyService.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _companyService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("{searchText}")]
        public async Task<IActionResult> GetBySearchText([FromRoute] string searchText)
        {
            var companies = await _companyService.GetAllAsync();

            return Ok(companies.Where(m => m.Name.ToLower().Contains(searchText.ToLower())));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([Required] int id)
        {
            await _companyService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _companyService.CreateAsync(request);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CompanyUpdateDto request)
        {
            await _companyService.UpdateAsync(id, request);

            return Ok();
        }
    }
}
