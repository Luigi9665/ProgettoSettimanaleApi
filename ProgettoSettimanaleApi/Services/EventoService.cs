using Microsoft.EntityFrameworkCore;
using ProgettoSettimanaleApi.Model.DTOs;
using ProgettoSettimanaleApi.Model.Entity;

namespace ProgettoSettimanaleApi.Services
{
    public class EventoService : ServiceBase
    {

        public EventoService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<EventListDto>> GetAllEventi()
        {
            return await _applicationDbContext.Eventi.AsNoTracking()
                .Include(e => e.Artista)
                .Include(e => e.Biglietti)
                .Select(e => new EventListDto
                {
                    EventoId = e.EventoId,
                    Titolo = e.Titolo,
                    Data = e.Data,
                    Luogo = e.Luogo,
                    Artista = new ArtistDto
                    {
                        Nome = e.Artista.Nome,
                        Genere = e.Artista.Genere,
                        Biografia = e.Artista.Biografia
                    },
                    Biglietti = e.Biglietti.Select(b => new BigliettiEventoDto
                    {
                        EventoId = b.EventoId,
                        UserId = b.UserId
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Evento?> GetEventoById(Guid id)
        {
            return await _applicationDbContext.Eventi.FirstOrDefaultAsync(e => e.EventoId == id);
        }

        public async Task<EventListDto> GetEventoByIdResponse(Guid id)
        {
            return await _applicationDbContext.Eventi
                .Where(e => e.EventoId == id)
                .Select(e => new EventListDto
                {
                    EventoId = e.EventoId,
                    Titolo = e.Titolo,
                    Data = e.Data,
                    Luogo = e.Luogo,
                    Artista = new ArtistDto
                    {
                        Nome = e.Artista.Nome,
                        Genere = e.Artista.Genere,
                        Biografia = e.Artista.Biografia
                    },
                    Biglietti = e.Biglietti.Select(b => new BigliettiEventoDto
                    {
                        EventoId = b.EventoId,
                        UserId = b.UserId
                    }).ToList()
                })
                .FirstOrDefaultAsync();
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
