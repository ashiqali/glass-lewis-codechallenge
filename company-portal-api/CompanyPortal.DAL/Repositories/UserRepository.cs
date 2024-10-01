using CompanyPortal.DAL.DataContext;
using CompanyPortal.DAL.Entities;
using CompanyPortal.DAL.Repositories.IRepositories;

namespace CompanyPortal.DAL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly CompanyPortalDbContext _companyPortalDbContext;
    public UserRepository(CompanyPortalDbContext companyPortalDbContext) : base(companyPortalDbContext)
    {
        _companyPortalDbContext = companyPortalDbContext;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _ = _companyPortalDbContext.Update(user);

        // Ignore password property update for user
        _companyPortalDbContext.Entry(user).Property(x => x.Password).IsModified = false;

        await _companyPortalDbContext.SaveChangesAsync();
        return user;
    }
}
