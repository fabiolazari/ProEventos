using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Intefaces;
using ProEventos.Persistence.Models;

namespace ProEventos.Application.Services
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IPalestranteRepository _palestranteRepository;

        public PalestranteService(IBaseRepository baseRepository, IPalestranteRepository palestranteRepository)
        {
            _baseRepository = baseRepository;
            _palestranteRepository = palestranteRepository;
        }

        public async Task<Palestrante> AddPalestrante(Palestrante model)
        {
           try
            {
                _baseRepository.Add<Palestrante>(model);
                if (await _baseRepository.SaveChangesAsync())
                    return await _palestranteRepository.GetPalestranteByIdAsync(model.Id, false);
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }

        public async Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(model.Id, false);
                if (palestrante == null)
                    return null;
                
                model.Id = palestrante.Id;

                _baseRepository.Update<Palestrante>(model);
                if (await _baseRepository.SaveChangesAsync())
                    return await _palestranteRepository.GetPalestranteByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }

        public async Task<bool> DeletePalestrantes(int palestranteId)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByIdAsync(palestranteId, false);
                if (palestrante == null)
                    throw new Exception("Palestrante para exclusão não encontrado");
                
                _baseRepository.Delete<Palestrante>(palestrante);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }

        }

        public async Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
        {
            try
            {
                var palestrantes = await _palestranteRepository.GetAllPalestrantesAsync(pageParams, includeEventos);
                if (palestrantes == null)
                    return null;

                /* var resultado = _mapper.Map<PalestranteDto[]>(eventos);
                var resultado = _mapper.Map<PageList<PalestranteDto>>(eventos);

                 resultado.CurrentPage = palestrantes.CurrentPage;
                resultado.TotalPages = palestrantes.TotalPages;
                resultado.PageSize = palestrantes.PageSize;
                resultado.TotalCount = palestrantes.TotalCount;
                */

                return palestrantes;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }
/*
        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            try
            {
                var palestrantes = await _palestranteRepository.GetAllPalestrantesByNomeAsync(nome, includeEventos);
                if (palestrantes == null)
                    return null;

                return palestrantes;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }
*/
        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            try
            {
                var palestrantes = await _palestranteRepository.GetPalestranteByIdAsync(palestranteId, includeEventos);
                if (palestrantes == null)
                    return null;

                return palestrantes;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error: {exception.Message}");
            }
        }
    }
}