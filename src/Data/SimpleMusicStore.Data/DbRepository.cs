using System;
using System.Linq;
using System.Threading.Tasks;
using SimpleMusicStore.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace SimpleMusicStore.Data
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly SimpleMusicStoreContext context;
        private readonly DbSet<TEntity> dbSet;



        public DbRepository(SimpleMusicStoreContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }



        public Task AddAsync(TEntity entity)
        {
            return this.dbSet.AddAsync(entity);
        }



        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }



        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }



        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }



        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
