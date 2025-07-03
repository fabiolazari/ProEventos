using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using ProEventos.Persistence.Models;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;

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
        public async Task<IActionResult> Get([FromQuery] PageParams pagePrams, [FromQuery] bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(pagePrams, includePalestrantes);
                if (eventos == null)
                    return NoContent();

                Response.AddPagination(eventos.CurrentPage, eventos.PageSize, eventos.TotalCount, eventos.TotalPages);

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
                    return NoContent();

                return Ok(evento);    
            }
            catch (Exception exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento.  {exception.Message}");
            }
        }

        /*     [HttpGet("{tema}/tema")]
             public async Task<IActionResult> GetByTema(string tema, [FromQuery] bool includePalestrantes)
             {
                 try
                 {
                     var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, includePalestrantes);
                     if (eventos == null)
                         return NoContent();

                     return Ok(eventos);    
                 }
                 catch (Exception exception)
                 {
                     return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos.  {exception.Message}");
                 }
             }
     */
        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if (evento == null)
                    return NoContent();

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
        public async Task<IActionResult> Put(int id, EventoDto model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null)
                    return NoContent();

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
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null)
                    return NoContent();
                
                return await _eventoService.DeleteEvento(id) 
                    ?  Ok(new { message = "Deletado" })
                    : throw new Exception("Ocorreu um problema específico ao tentar excluir evento.");
            }
            catch (Exception exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir o evento.  {exception.Message}");
            }
        }        
    }
}
