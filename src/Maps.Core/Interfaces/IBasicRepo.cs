namespace Maps.src.Maps.Core.Interfaces
{
    public interface IBasicRepo<T> where T : class
    {
        Task<List<T>>? GetAllAsync();
        Task<T>? GetByIdAsync(int id);
        Task<List<T>> GetByNameAsync(string name);

        Task<T> AddAsync(T entity);

        Task<T>? UpdateAsync(T entity);

        Task<T>? DeleteAsync(int id);
        Task<T>? DeleteAsync(T entity);
        Task<List<T>>? DeleteAllAsync(List<T> entities);
    }
}
