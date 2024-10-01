using CompanyPortal.BLL.Services.IServices;
using CompanyPortal.BLL.Utilities.CustomExceptions;
using CompanyPortal.DTO.DTOs.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.API.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        // POST: api/v1/company
        /// <summary>
        /// Create Company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDTO company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(company.Isin) || !IsValidIsin(company.Isin))
            {
                return BadRequest("Invalid ISIN format.");
            }

            try
            {
                var createdCompany = await _companyService.CreateCompanyAsync(company);
                return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new company.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        // GET: api/v1/company/{id}
        /// <summary>
        /// Get Company By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompanyById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _companyService.GetCompanyByIdAsync(id, cancellationToken);
                if (company == null)
                {
                    return NotFound(new { message = "Company not found" });
                }
                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching company with ID {CompanyId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        // GET: api/v1/company/isin/{isin}
        /// <summary>
        /// Get Company By Isin
        /// </summary>
        /// <param name="isin"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("isin/{isin}")]
        public async Task<IActionResult> GetCompanyByIsin(string isin, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _companyService.GetCompanyByIsinAsync(isin, cancellationToken);
                if (company == null)
                {
                    return NotFound(new { message = "Company not found" });
                }
                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching company with ISIN {Isin}.", isin);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // GET: api/v1/company
        /// <summary>
        /// Get All Companies
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies(CancellationToken cancellationToken)
        {
            try
            {
                var companies = await _companyService.GetAllCompaniesAsync(cancellationToken);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all companies.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // PUT: api/v1/company/{id}
        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyDTO company)
        {
            if (id != company.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedCompany = await _companyService.UpdateCompanyAsync(company);
                return Ok(updatedCompany);
            }
            catch (CompanyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Company with ID {CompanyId} not found.", id);
                return NotFound(new { message = "Company not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating company with ID {CompanyId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // DELETE: api/v1/company/{id}
        /// <summary>
        /// Delete Company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                await _companyService.DeleteCompanyAsync(id);
                return NoContent();
            }
            catch (CompanyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Company with ID {CompanyId} not found.", id);
                return NotFound(new { message = "Company not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting company with ID {CompanyId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        /// <summary>
        /// IsValidIsin
        /// </summary>
        /// <param name="isin"></param>
        /// <returns></returns>
        private bool IsValidIsin(string isin)
        {
            return isin.Length == 12 && char.IsLetter(isin[0]) && char.IsLetter(isin[1]);
        }
    }

}
