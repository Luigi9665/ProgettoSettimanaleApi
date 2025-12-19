using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanaleApi.Model.Entity;
using ProgettoSettimanaleApi.Services;
using System.Security.Claims;

namespace ProgettoSettimanaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,User")]
    public class BigliettoController : ControllerBase
    {

        private readonly BigliettoService _bigliettoService;

        public BigliettoController(BigliettoService bigliettoService)
        {
            _bigliettoService = bigliettoService;
        }

        [HttpGet("AllBiglietti")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAllBiglietti()
        {
            try
            {
                return Ok(await _bigliettoService.GetAllBiglietti());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("BigliettiByUser")]
        public async Task<IActionResult> GetBigliettiByUser()
        {
            // Prendo l'ID dell'utente dal token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine("UserId: " + userId);

            if (userId == null)
            {
                return Unauthorized();
            }

            var biglietti = await _bigliettoService.GetBigliettiByUserId(userId);

            if (biglietti == null)
            {
                return NotFound();
            }

            try
            {
                return Ok(biglietti);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("BuyTicket")]
        public async Task<IActionResult> BuyTicket(Guid eventoId)
        {
            try
            {
                // Prendo l'ID dell'utente dal token
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Console.WriteLine("UserId: " + userId);

                if (userId == null)
                    return Unauthorized();

                // Crea il biglietto
                var biglietto = new Biglietto
                {
                    BigliettoId = Guid.NewGuid(),
                    EventoId = eventoId,
                    UserId = userId,
                };

                if (await _bigliettoService.AddBiglietto(biglietto))
                {
                    return Ok(biglietto);
                }
                else
                {
                    return BadRequest("Non è stato possibile acquistare il biglietto.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
