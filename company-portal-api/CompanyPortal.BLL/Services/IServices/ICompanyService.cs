using CompanyPortal.DTO.DTOs.Company;

namespace CompanyPortal.BLL.Services.IServices
{
    public interface ICompanyService
    {
        Task<CompanyDTO> CreateCompanyAsync(CompanyDTO companyDTO);
        Task<CompanyDTO> GetCompanyByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<CompanyDTO> GetCompanyByIsinAsync(string isin, CancellationToken cancellationToken = default);
        Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync(CancellationToken cancellationToken = default);
        Task<CompanyDTO> UpdateCompanyAsync(CompanyDTO companyDTO);
        Task DeleteCompanyAsync(int id);
    }
}
