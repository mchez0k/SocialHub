using System.Linq.Expressions;

namespace SocialHub.Shared;

public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Получаем сущность по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetById(Guid id);

    /// <summary>
    /// Получаем все сущности
    /// </summary>
    /// <param name="filter">Фильтр LINQ</param>
    /// <returns></returns>
    IEnumerable<TEntity> Get(Expression<Func<TEntity,
        bool>>? filter = null,
        Func<IQueryable<TEntity>,
        IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");
    
    /// <summary>
    /// Создаём сущность
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task CreateAsync(TEntity entity);
    
    /// <summary>
    /// Обновляем сущность
    /// </summary>
    Task UpdateAsync(TEntity entity);
    
    /// <summary>
    /// Удаляем сущность
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    void Delete(TEntity entity);
    
    /// <summary>
    /// Сохраняем изменения
    /// </summary>
    /// <returns></returns>
    Task SaveChangesAsync();
}