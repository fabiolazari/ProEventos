using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Intefaces;

namespace ProEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IBaseRepository baseRepository, IEventoRepository eventoRepository)
        {
            _baseRepository = baseRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _baseRepository.Add<Evento>(model);
                if (await _baseRepository.SaveChangesAsync())
                    return await _eventoRepository.GetEventoByIdAsync(model.Id, false);
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(model.Id, false);
                if (evento == null)
                    return null;
                
                model.Id = evento.Id;

                _baseRepository.Update<Evento>(model);
                if (await _baseRepository.SaveChangesAsync())
                    return await _eventoRepository.GetEventoByIdAsync(model.Id, false);

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

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync(includePalestrantes);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }

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

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }
    }
}