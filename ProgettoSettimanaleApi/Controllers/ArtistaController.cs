using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanaleApi.Model.DTOs;
using ProgettoSettimanaleApi.Model.Entity;
using ProgettoSettimanaleApi.Services;

namespace ProgettoSettimanaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class ArtistaController : ControllerBase
    {

        private readonly ArtistaService _artistaService;

        public ArtistaController(ArtistaService artistaService)
        {
            _artistaService = artistaService;
        }

        [HttpGet("Artists")]
        public async Task<IActionResult> GetAllArtist()
        {
            try
            {
                return Ok(await _artistaService.GetAllArtisti());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CreateArtist")]
        public async Task<IActionResult> CreateArtist(ArtistDto artistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid");
            }

            Artista artista = new Artista
            {
                ArtistId = Guid.NewGuid(),
                Nome = artistDto.Nome,
                Genere = artistDto.Genere,
                Biografia = artistDto.Biografia
            };

            try
            {
                if (await _artistaService.AddArtista(artista))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("UpdateArtist/{id}")]
        public async Task<IActionResult> UpdateArtist(Guid id, ArtistDto artistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid");
            }

            try
            {
                if (await _artistaService.UpdateArtista(id, artistDto))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("RemoveArtist/{id}")]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {
            try
            {
                if (await _artistaService.DeleteArtista(id))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
