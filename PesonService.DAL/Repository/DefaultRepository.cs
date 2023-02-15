using Microsoft.EntityFrameworkCore;
using PesonService.DAL.Contract;

namespace PesonService.DAL.Repository
{
    public class DefaultRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DefaultDbContext _dbContext;

        protected DbSet<T> Table => _dbContext.Set<T>();

        public DefaultRepository(DefaultDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await Table.FirstAsync(entity => entity.Id == id, cancellationToken);

            Table.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await Table.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

            return entity;
        }

        public virtual IQueryable<T> GetAll()
        {
            return Table;
        }

        public virtual async Task InsertAsync(T entity, CancellationToken cancellationToken)
        {
            Table.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            Table.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
