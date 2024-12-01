using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Intefaces;
using ProEventos.Persistence.Models;

namespace ProEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IMapper _mapper;

        public EventoService(
            IBaseRepository baseRepository,
            IEventoRepository eventoRepository, 
            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEvento(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _baseRepository.Add<Evento>(evento);
                if (await _baseRepository.SaveChangesAsync())
                    return _mapper.Map<EventoDto>(await _eventoRepository.GetEventoByIdAsync(evento.Id, false));

                return null;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, false);
                if (evento == null)
                    return null;
                
                model.Id = evento.Id;

                _mapper.Map(model, evento);

                 _baseRepository.Update<Evento>(evento);
                 if (await _baseRepository.SaveChangesAsync())
                    return _mapper.Map<EventoDto>(await _eventoRepository.GetEventoByIdAsync(evento.Id, false));

                return null;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, false);
                if (evento == null)
                    throw new Exception("Evento para exclusão não encontrado");
                
                _baseRepository.Delete<Evento>(evento);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }

        public async Task<PageList<EventoDto>> GetAllEventosAsync(PageParams pageParams, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync(pageParams, includePalestrantes);
                if (eventos == null)
                    return null;

                var eventoDto = _mapper.Map<PageList<EventoDto>>(eventos);
 

                /* var resultado = _mapper.Map<EventoDto[]>(eventos);
                 var resultado = _mapper.Map<PageList<EventoDto>>(eventos);

                 resultado.CurrentPage = eventos.CurrentPage;
                 resultado.TotalPages = eventos.TotalPages;
                 resultado.PageSize = eventos.PageSize;
                 resultado.TotalCount = eventos.TotalCount;

                 */

                return eventoDto;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }
/*
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }
*/
        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (evento == null)
                    return null;

                var eventoDto = _mapper.Map<EventoDto>(evento);

                return eventoDto;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }
    }
}