using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain.Models;
using ProEventos.Application.Interfaces;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Builder;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
       
        public EventoController(IEventoService eventoService)
            => _eventoService = eventoService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(includePalestrantes);
                if (eventos == null)
                    return NotFound("Nenhum evento encontrado");

                return Ok(eventos);    
            }
            catch (Exception exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar reccuperar eventos.  {exception.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includePalestrantes)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, includePalestrantes);
                if (evento == null)
                    return NotFound("Evento encontrado");

                return Ok(evento);    
            }
            catch (Exception exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento.  {exception.Message}");
            }
        }
        
        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema, [FromQuery] bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null)
                    return NotFound("Nenhum evento encontrado");

                return Ok(eventos);    
            }
            catch (Exception exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos.  {exception.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if (evento == null)
                    return BadRequest("Erro ao tentar adicionar evento.");

                //return new ObjectResult(evento) { StatusCode = StatusCodes.Status201Created };
                //return new ObjectResult(null) { StatusCode = StatusCodes.Status201Created };
                return new ObjectResult(new { eventoid = evento.Id}) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o evento.  {exception.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null)
                    return BadRequest("Erro ao tentar alterar evento.");

                return NoContent();
            }
            catch (Exception exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar o evento.  {exception.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _eventoService.DeleteEvento(id) ?  NoContent(): BadRequest("Erro ao tentar excluir evento.");
            }
            catch (Exception exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir o evento.  {exception.Message}");
            }
        }        
    }
}
