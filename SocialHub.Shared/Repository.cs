using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SocialHub.Shared;

public class Repository<TEntity>(DbContext context) : IRepository<TEntity>, IDisposable where TEntity : BaseEntity
{
    public IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>,
        IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {

        IQueryable<TEntity> query = context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }
    
    public async Task<TEntity?> GetById(Guid id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }
    
    public async Task CreateAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity); 
        await Task.CompletedTask;
    }
    
    public async Task UpdateAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    public void Delete(TEntity entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            context.Set<TEntity>().Attach(entity);
        }
        context.Set<TEntity>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    
    private bool disposed = false;
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}