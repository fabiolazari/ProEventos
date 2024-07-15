using System.Threading.Tasks;
using ProEventos.Domain.Models;

namespace ProEventos.Application.Interfaces
{
    public interface IPalestranteService
    {
        Task<Palestrante> AddPalestrante(Palestrante model);
        Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model);
        Task<bool> DeletePalestrantes(int palestranteId);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false);
    }
}