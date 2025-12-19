using ProgettoSettimanaleApi.Model.Entity;

namespace ProgettoSettimanaleApi.Services
{
    public abstract class ServiceBase
    {

        protected ApplicationDbContext _applicationDbContext;

        public ServiceBase(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async Task<bool> SaveChangeAsync()
        {
            bool result = false;

            try
            {
                result = await _applicationDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
