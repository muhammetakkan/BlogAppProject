using BlogApp.Entities;

namespace BlogApp.Dal.Abstract
{
    public interface ITagRepository<T> where T : class
    {
        Task Create(T entity);
        IQueryable<T> Tags { get; }
        Task Update(T entity);
        Task Delete(int? id);
    }
}
