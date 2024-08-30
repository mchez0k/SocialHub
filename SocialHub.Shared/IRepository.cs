namespace SocialHub.Shared;

public interface IRepository<TEntity> where TEntity : BaseEntity
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
    /// <returns></returns>
    Task<List<TEntity>> GetAsync();
    
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
    Task DeleteAsync(Guid id);
    
    /// <summary>
    /// Сохраняем изменения
    /// </summary>
    /// <returns></returns>
    Task SaveChangesAsync();
}