using AutoMapper;
using CompanyPortal.BLL.Services.IServices;
using CompanyPortal.BLL.Utilities.CustomExceptions;
using CompanyPortal.DAL.Entities;
using CompanyPortal.DAL.Repositories.IRepositories;
using CompanyPortal.DTO.DTOs.Company;
using Microsoft.Extensions.Logging;

namespace CompanyPortal.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CompanyDTO> CreateCompanyAsync(CompanyDTO companyDTO)
        {
            _logger.LogInformation("Creating a new company with ISIN = {Isin}", companyDTO.Isin);

            // Check if a company with the same ISIN already exists
            var existingCompany = await _companyRepository.GetAsync(c => c.Isin == companyDTO.Isin);
            if (existingCompany != null)
            {
                throw new DuplicateIsinException($"A company with ISIN {companyDTO.Isin} already exists.");
            }

            var company = _mapper.Map<Company>(companyDTO);

            company.CreatedDate = DateTime.UtcNow;
            company.ModifiedDate = DateTime.UtcNow;

            var createdCompany = await _companyRepository.AddAsync(_mapper.Map<Company>(companyDTO));
            _logger.LogInformation("Company with ISIN = {Isin} created successfully", companyDTO.Isin);

            return _mapper.Map<CompanyDTO>(createdCompany);
        }

        public async Task<CompanyDTO> GetCompanyByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching company with ID = {Id}", id);
            var company = await _companyRepository.GetAsync(x => x.Id == id, cancellationToken);

            if (company is null)
            {
                _logger.LogError("Company with ID = {Id} was not found", id);
                throw new CompanyNotFoundException();
            }

            return _mapper.Map<CompanyDTO>(company);
        }

        public async Task<CompanyDTO> GetCompanyByIsinAsync(string isin, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching company with ISIN = {Isin}", isin);
            var company = await _companyRepository.GetCompanyByIsinAsync(isin, cancellationToken);

            if (company is null)
            {
                _logger.LogError("Company with ISIN = {Isin} was not found", isin);
                throw new CompanyNotFoundException($"A company with ISIN {isin} not found.");
            }

            return _mapper.Map<CompanyDTO>(company);
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching all companies");
            var companies = await _companyRepository.GetListAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("Returned {Count} companies", companies.Count());
            return _mapper.Map<IEnumerable<CompanyDTO>>(companies);
        }

        public async Task<CompanyDTO> UpdateCompanyAsync(CompanyDTO companyDTO)
        {
            _logger.LogInformation("Updating company with ISIN = {Isin}", companyDTO.Isin);
            var company = _mapper.Map<Company>(companyDTO);
            company.ModifiedDate = DateTime.UtcNow;

            var updatedCompany = await _companyRepository.UpdateAsync(company);
            _logger.LogInformation("Company with ISIN = {Isin} updated successfully", companyDTO.Isin);
            return _mapper.Map<CompanyDTO>(updatedCompany);
        }

        public async Task DeleteCompanyAsync(int id)
        {
            _logger.LogInformation("Deleting company with ID = {Id}", id);
            var companyToDelete = await _companyRepository.GetAsync(x => x.Id == id);

            if (companyToDelete is null)
            {
                _logger.LogError("Company with ID = {Id} was not found", id);
                throw new CompanyNotFoundException($"A company with Id {id} not found.");
            }

            await _companyRepository.DeleteAsync(companyToDelete);
            _logger.LogInformation("Company with ID = {Id} deleted successfully", id);
        }
    }
}
