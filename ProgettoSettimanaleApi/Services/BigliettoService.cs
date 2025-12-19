using Microsoft.EntityFrameworkCore;
using ProgettoSettimanaleApi.Model.Entity;

namespace ProgettoSettimanaleApi.Services
{
    public class BigliettoService : ServiceBase
    {

        public BigliettoService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<Biglietto>> GetAllBiglietti()
        {
            return await _applicationDbContext.Biglietti.AsNoTracking().ToListAsync();

        }

        public async Task<Biglietto?> GetBigliettoById(Guid id)
        {
            return await _applicationDbContext.Biglietti.FirstOrDefaultAsync(b => b.BigliettoId == id);
        }

        public async Task<List<Biglietto>> GetBigliettiByUserId(string id)
        {
            return await _applicationDbContext.Biglietti.Where(b => b.UserId == id).AsNoTracking().ToListAsync();
        }

        public async Task<bool> AddBiglietto(Biglietto biglietto)
        {
            await _applicationDbContext.Biglietti.AddAsync(biglietto);
            return await SaveChangeAsync();
        }

        public async Task<bool> DeleteBiglietto(Biglietto biglietto)
        {
            _applicationDbContext.Biglietti.Remove(biglietto);
            return await SaveChangeAsync();
        }

        public async Task<bool> UpdateBiglietto(Biglietto biglietto)
        {
            var existingBiglietto = await GetBigliettoById(biglietto.BigliettoId);

            if (existingBiglietto == null)
            {
                return false;
            }


            existingBiglietto.BigliettoId = biglietto.BigliettoId;
            existingBiglietto.EventoId = biglietto.EventoId;
            existingBiglietto.UserId = biglietto.UserId;

            return await SaveChangeAsync();
        }




    }
}
