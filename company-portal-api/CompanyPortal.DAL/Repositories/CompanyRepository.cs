using CompanyPortal.DAL.DataContext;
using CompanyPortal.DAL.Entities;
using CompanyPortal.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.DAL.Repositories
{
    public class CompanyRepository : GenericRepository<Company>,  ICompanyRepository
    {        

        private readonly CompanyPortalDbContext _context;
        public CompanyRepository(CompanyPortalDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Company> GetCompanyByIsinAsync(string isin, CancellationToken cancellationToken = default)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Isin == isin, cancellationToken);
        }
    }
}
