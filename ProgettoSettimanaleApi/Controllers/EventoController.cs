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
    public class EventoController : ControllerBase
    {

        private readonly EventoService _eventoService;

        public EventoController(EventoService eventoService)
        {
            _eventoService = eventoService;

        }

        [HttpGet("AllEventi")]
        public async Task<IActionResult> GetAllEventi()
        {
            try
            {
                return Ok(await _eventoService.GetAllEventi());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("EventoById/{id}")]
        public async Task<IActionResult> GetEventoById(Guid id)
        {
            try
            {
                var evento = await _eventoService.GetEventoById(id);
                if (evento == null)
                {
                    return NotFound();
                }
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpPost("CreateEvento")]
        public async Task<IActionResult> CreateEvento(EventoDto eventoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid");
            }
            Evento evento = new Evento
            {
                EventoId = Guid.NewGuid(),
                Titolo = eventoDto.Titolo,
                Data = eventoDto.Data,
                Luogo = eventoDto.Luogo,
                ArtistaId = eventoDto.ArtistaId
            };
            try
            {
                if (await _eventoService.AddEvento(evento))
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

        [HttpPut("UpdateEvento/{id}")]
        public async Task<IActionResult> UpdateEvento(Guid id, EventoDto eventoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not valid");
            }
            Evento evento = new Evento
            {
                EventoId = id,
                Titolo = eventoDto.Titolo,
                Data = eventoDto.Data,
                Luogo = eventoDto.Luogo,
                ArtistaId = eventoDto.ArtistaId
            };
            try
            {
                if (await _eventoService.UpdateEvento(id, evento))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("DeleteEvento/{id}")]
        public async Task<IActionResult> DeleteEvento(Guid id)
        {
            try
            {
                var evento = await _eventoService.GetEventoById(id);
                if (evento == null)
                {
                    return NotFound();
                }
                if (await _eventoService.DeleteEvento(evento))
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
