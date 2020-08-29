using Microsoft.EntityFrameworkCore;
using Model.Domain.Interfaces;
using Model.Infra.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model.Infra.Data.Repository.Base
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class, new()
    {
        private DatabaseContext Context { get; }
        private DbSet<T> Model { get; }
        public RepositoryBase(DatabaseContext context)
        {
            Context = context;
            Model = context.Set<T>();
        }
        public async Task<T> AddAsync(T model)
        {
            await Model.AddAsync(model);
            await SaveAsync();
            return model;
        }

        public async Task<T> GetAsync(params object[] keys)
        {
            return await Context
                .Set<T>()
                .FindAsync(keys);
        }

        public IAsyncEnumerable<T> GetAsync()
        {
            return Model
                .AsNoTracking()
                .AsAsyncEnumerable<T>();
        }

        public async Task<bool> RemoveAsync<TKey>(TKey id)
        {
            var model = await GetAsync(id);
            return await RemoveAsync<T>(model);
        }

        public async Task<bool> RemoveAsync(T model)
        {
            Context.Remove<T>(model);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(T model)
        {
            Model.Update(model);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditAsync(T model)
        {
            Context.Entry<T>(model).State = EntityState.Modified;
            return await SaveAsync();
        }
    }
}
