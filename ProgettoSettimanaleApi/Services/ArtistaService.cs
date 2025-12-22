using Microsoft.EntityFrameworkCore;
using ProgettoSettimanaleApi.Model.DTOs;
using ProgettoSettimanaleApi.Model.DTOs.Responses;
using ProgettoSettimanaleApi.Model.Entity;

namespace ProgettoSettimanaleApi.Services
{
    public class ArtistaService : ServiceBase
    {

        public ArtistaService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<ArtistaResponse>> GetAllArtisti()
        {
            return await _applicationDbContext.Artisti
                .AsNoTracking()
                .Select(a => new ArtistaResponse
                {
                    ArtistaId = a.ArtistId,
                    Nome = a.Nome,
                    Genere = a.Genere,
                    Biografia = a.Biografia,
                    Eventi = a.Eventi != null ? a.Eventi.Select(e => new EventForArtistResponse
                    {
                        EventoId = e.EventoId,
                        Titolo = e.Titolo,
                        Data = e.Data,
                        Luogo = e.Luogo
                    }).ToList() : new List<EventForArtistResponse>(),
                    Biglietti = a.Eventi != null
                        ? a.Eventi.SelectMany(e => e.Biglietti)
                            .Select(b => new BigliettiEventoDto
                            {
                                EventoId = b.EventoId,
                                UserId = b.UserId
                            }).ToList()
                        : new List<BigliettiEventoDto>()

                })
                .ToListAsync();
        }

        public async Task<Artista?> GetArtistaById(Guid id)
        {
            return await _applicationDbContext.Artisti.FirstOrDefaultAsync(a => a.ArtistId == id);
        }

        public async Task<bool> AddArtista(Artista artista)
        {
            await _applicationDbContext.Artisti.AddAsync(artista);
            return await SaveChangeAsync();
        }

        public async Task<bool> DeleteArtista(Guid id)
        {
            var artista = await GetArtistaById(id);
            _applicationDbContext.Artisti.Remove(artista);
            return await SaveChangeAsync();
        }

        public async Task<bool> UpdateArtista(Guid id, ArtistDto artista)
        {
            var existingArtista = await GetArtistaById(id);
            if (existingArtista == null)
            {
                return false;
            }
            existingArtista.Nome = artista.Nome;
            existingArtista.Genere = artista.Genere;
            existingArtista.Biografia = artista.Biografia;
            return await SaveChangeAsync();
        }


    }
}
