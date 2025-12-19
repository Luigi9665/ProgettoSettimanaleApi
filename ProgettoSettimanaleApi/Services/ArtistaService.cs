using Microsoft.EntityFrameworkCore;
using ProgettoSettimanaleApi.Model.Entity;

namespace ProgettoSettimanaleApi.Services
{
    public class ArtistaService : ServiceBase
    {

        public ArtistaService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<Artista>> GetAllArtisti()
        {
            return await _applicationDbContext.Artisti.AsNoTracking().ToListAsync();
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

        public async Task<bool> DeleteArtista(Artista artista)
        {
            _applicationDbContext.Artisti.Remove(artista);
            return await SaveChangeAsync();
        }

        public async Task<bool> UpdateArtista(Guid id, Artista artista)
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
