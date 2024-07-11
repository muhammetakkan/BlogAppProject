using BlogApp.Entities;

namespace BlogApp.Dal.Abstract
{
    public interface IUserRepository<T> where T : class
    {
        Task Create(T entity);
        IQueryable<T> Users { get; }
        Task Delete(int? id);
        Task Update(T entity);

    }
}
