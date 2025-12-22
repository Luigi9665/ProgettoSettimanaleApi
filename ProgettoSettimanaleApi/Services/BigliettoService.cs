using Microsoft.EntityFrameworkCore;
using ProgettoSettimanaleApi.Model.DTOs;
using ProgettoSettimanaleApi.Model.DTOs.Responses;
using ProgettoSettimanaleApi.Model.Entity;
using System.ComponentModel;

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

        public async Task<List<BigliettoResponse>> GetBigliettiByUserId(string id, string useremail)
        {
            return await _applicationDbContext.Biglietti.Where(b => b.UserId == id)
                .AsNoTracking().
                Select(b => new BigliettoResponse
                {
                    BigliettoId = b.BigliettoId,
                    EventoId = b.EventoId,
                    UserId = id,
                    UserName = useremail,
                    Evento = b.Evento != null ? new EventoDto
                    {
                        Titolo = b.Evento.Titolo,
                        Data = b.Evento.Data,
                        Luogo = b.Evento.Luogo,
                        ArtistaId = b.Evento.ArtistaId
                    } : null,
                    Artista = b.Evento != null && b.Evento.Artista != null ? new ArtistDto
                    {
                        Nome = b.Evento.Artista.Nome,
                        Genere = b.Evento.Artista.Genere,
                        Biografia = b.Evento.Artista.Biografia
                    } : null
                })
                 .
                ToListAsync();
        }

        public async Task<BigliettoResponse> GetBigliettoResponse(Biglietto biglietto, string userid, string useremail)
        {

            return await _applicationDbContext.Biglietti
                .Where(b => b.BigliettoId == biglietto.BigliettoId)
                .Select(b => new BigliettoResponse
                {
                    BigliettoId = b.BigliettoId,
                    EventoId = b.EventoId,
                    UserId = userid,
                    UserName = useremail,
                    Evento = b.Evento != null ? new EventoDto
                    {
                        Titolo = b.Evento.Titolo,
                        Data = b.Evento.Data,
                        Luogo = b.Evento.Luogo,
                        ArtistaId = b.Evento.ArtistaId
                    } : null,
                    Artista = b.Evento != null && b.Evento.Artista != null ? new ArtistDto
                    {
                        Nome = b.Evento.Artista.Nome,
                        Genere = b.Evento.Artista.Genere,
                        Biografia = b.Evento.Artista.Biografia
                    } : null
                })
                .FirstOrDefaultAsync();

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
