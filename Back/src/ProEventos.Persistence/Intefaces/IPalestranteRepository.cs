using System.Threading.Tasks;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence.Intefaces
{
    public interface IPalestranteRepository
    {
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false);
        Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false);
       // Task<PageList<Palestrante>> GetAllPalestrantesByNomeAsync(PageParams pageParams, string nome, bool includeEventos = false);
    }
}