using Microsoft.EntityFrameworkCore;
using marketplace_backend.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace marketplace_backend.Repositories
{
    public class BaseRepositorySQL<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly MarketplaceBackendDbContext context;
        public BaseRepositorySQL(MarketplaceBackendDbContext context)
        {
            this.context = context;
        }


        public async Task<TEntity?> InsertAsync(TEntity entity)
        {
            try
            {
                EntityEntry<TEntity> insertedEntity = await context.Set<TEntity>().AddAsync(entity);
                await context.SaveChangesAsync();
                return insertedEntity.Entity;
            }
            catch
            {
                return null;
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IList<TEntity>> SearchForAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }


        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> SaveAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            TEntity? ent = SearchForAsync(predicate).Result.FirstOrDefault();

            if (ent == null) return false;

            context.Set<TEntity>().Update(ent);
            await SaveChangesAsync();
            return true;
        }

        protected async Task SaveChangesAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbUpdateException(ex.InnerException.Message);
            }
        }
    }
}
