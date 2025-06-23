namespace Domain.IRepositories;

public interface IGenericRepository<TEntity> where TEntity:class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
}