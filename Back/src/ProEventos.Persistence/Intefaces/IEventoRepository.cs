using System.Threading.Tasks;
using ProEventos.Domain.Models;

namespace ProEventos.Persistence.Intefaces
{
    public interface IEventoRepository
    {
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
    }
}