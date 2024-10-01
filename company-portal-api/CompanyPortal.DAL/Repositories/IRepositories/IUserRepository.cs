using CompanyPortal.DAL.Entities;

namespace CompanyPortal.DAL.Repositories.IRepositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> UpdateUserAsync(User user);
}