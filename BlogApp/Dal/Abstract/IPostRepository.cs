using BlogApp.Entities;

namespace BlogApp.Dal.Abstract
{
    public interface IPostRepository<T> where T : class
    {
        Task Create(T entity);
        IQueryable<T> Posts { get; }
        Task Update(T entity);
        Task Delete(int? id);

    }
}
