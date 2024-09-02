using System.Linq.Expressions;

namespace SocialHub.Shared;

public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Получаем сущность по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получаем все сущности
    /// </summary>
    /// <param name="filter">Фильтр</param>
    /// <returns></returns>
    Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
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
    /// <param name="entity"></param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Сохраняем изменения
    /// </summary>
    /// <returns></returns>
    Task SaveChangesAsync();
}