using BlogApp.Dal.Abstract;

namespace BlogApp.Dal.Concrete
{
    public class EfCommentRepository<T> : ICommentRepository<T> where T : class
    {
        private readonly BlogAppContext _context;
        public EfCommentRepository(BlogAppContext blogAppContext)
        {
            _context = blogAppContext;
        }
        public IQueryable<T> Comments => _context.Set<T>();

        public async Task Create(T entity)
        {
            await _context.AddAsync(entity).ConfigureAwait(false);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
