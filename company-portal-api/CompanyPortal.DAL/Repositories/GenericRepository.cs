using CompanyPortal.DAL.DataContext;
using CompanyPortal.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CompanyPortal.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
{
    private readonly CompanyPortalDbContext _companyPortalDbContext;
    public GenericRepository(CompanyPortalDbContext companyPortalDbContext)
    {
        _companyPortalDbContext = companyPortalDbContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _companyPortalDbContext.AddAsync(entity);
        await _companyPortalDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entity)
    {
        await _companyPortalDbContext.AddRangeAsync(entity);
        await _companyPortalDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<int> DeleteAsync(TEntity entity)
    {
        _ = _companyPortalDbContext.Remove(entity);
        return await _companyPortalDbContext.SaveChangesAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        return await _companyPortalDbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        return await (filter == null ? _companyPortalDbContext.Set<TEntity>().ToListAsync(cancellationToken) : _companyPortalDbContext.Set<TEntity>().Where(filter).ToListAsync(cancellationToken));
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _ = _companyPortalDbContext.Update(entity);
        await _companyPortalDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entity)
    {
        _companyPortalDbContext.UpdateRange(entity);
        await _companyPortalDbContext.SaveChangesAsync();
        return entity;
    }
}
