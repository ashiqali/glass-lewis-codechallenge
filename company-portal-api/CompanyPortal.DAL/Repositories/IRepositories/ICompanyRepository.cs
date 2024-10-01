using CompanyPortal.DAL.Entities;

namespace CompanyPortal.DAL.Repositories.IRepositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company> GetCompanyByIsinAsync(string isin, CancellationToken cancellationToken = default);       
    }
}
