using Microsoft.EntityFrameworkCore;

namespace SocialHub.Shared;

public class Repository<TEntity>(DbContext context) : IRepository<TEntity>, IDisposable
    where TEntity : BaseEntity
{
    public async Task<List<TEntity>> GetAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
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

    public async Task DeleteAsync(Guid id)
    {
        TEntity? student = context.Set<TEntity>().Find(id);
        if (student == null)
        {
            throw new Exception("Entity not found"); // Если сущность не найдена, то выбрасываем исключение TODO: Проверить на полезность
        }
        context.Set<TEntity>().Remove(student);
        await Task.CompletedTask;
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