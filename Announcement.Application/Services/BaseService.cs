using Announcement.Persistence.Repositories;

namespace Announcement.Application.Services
{
    public interface IBaseService<TModel> where TModel : class
    {
        Task DeleteAsync(int id);
        List<TModel> Read();
    }
    public class BaseService<TModel> : IBaseService<TModel> where TModel : class
    {
        private readonly IBaseRepository<TModel> repository;
        public BaseService(IBaseRepository<TModel> _repository)
        {
            repository = _repository;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Argument is invalid.");

            try
            {
                await repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }

        public List<TModel> Read()
        {
            try
            {
                var announcements =  repository.Read();
                return announcements ?? new List<TModel>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving announcements.", ex);
            }
        }
    }
}
