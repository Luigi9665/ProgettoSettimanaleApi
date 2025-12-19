using Microsoft.EntityFrameworkCore;
using ProgettoSettimanaleApi.Model.Entity;

namespace ProgettoSettimanaleApi.Services
{
    public class EventoService : ServiceBase
    {

        public EventoService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<Evento>> GetAllEventi()
        {
            return await _applicationDbContext.Eventi.AsNoTracking().ToListAsync();
        }

        public async Task<Evento?> GetEventoById(Guid id)
        {
            return await _applicationDbContext.Eventi.FirstOrDefaultAsync(e => e.EventoId == id);
        }

        public async Task<bool> AddEvento(Evento evento)
        {
            await _applicationDbContext.Eventi.AddAsync(evento);
            return await SaveChangeAsync();
        }

        public async Task<bool> DeleteEvento(Evento evento)
        {
            _applicationDbContext.Eventi.Remove(evento);
            return await SaveChangeAsync();
        }

        public async Task<bool> UpdateEvento(Guid id, Evento evento)
        {
            var existingEvento = await GetEventoById(id);
            if (existingEvento == null)
            {
                return false;
            }
            existingEvento.Titolo = evento.Titolo;
            existingEvento.Data = evento.Data;
            existingEvento.Luogo = evento.Luogo;
            existingEvento.ArtistaId = evento.ArtistaId;
            return await SaveChangeAsync();
        }

    }
}
