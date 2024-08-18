using Announcement.Domain.Models;
using Announcement.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Announcement.Persistence.Repositories
{
    public interface IBaseRepository<TModel> where TModel : class
    {
        Task CreateAsync(TModel entity);
        Task UpdateAsync(TModel announcement);
        Task DeleteAsync(int id);
        List<TModel> Read();
        Task<TModel> ReadByIdAsync(int id);
    }

    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        private readonly AppDbContext dbContext;
        public BaseRepository(AppDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task CreateAsync(TModel entity)
        {
            await dbContext.Set<TModel>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            TModel? entity = await dbContext.Set<TModel>().FindAsync(id);

            if (entity != null)
            {
                dbContext.Set<TModel>().Remove(entity);
            }
            await dbContext.SaveChangesAsync();
        }

        public List<TModel> Read()
        {
            return dbContext.Set<TModel>().AsNoTracking().ToList();
        }

        public async Task<TModel> ReadByIdAsync(int id)
        {
            return await dbContext.Set<TModel>().FindAsync(id);
        }

        public async Task UpdateAsync(TModel entity)
        {
            dbContext.Set<TModel>().Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
