using BlogApp.Entities;

namespace BlogApp.Dal.Abstract
{
    public interface ICommentRepository<T> where T : class
    {
        Task Create(T entity);
        IQueryable<T> Comments { get; }
        Task Update(T entity);
        Task Delete(int? id);
    }
}
