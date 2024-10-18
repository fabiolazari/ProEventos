using System.Threading.Tasks;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Models;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<Evento> AddEvento(Evento model);
        Task<Evento> UpdateEvento(int eventoId, Evento model);
        Task<bool> DeleteEvento(int eventoId);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
        Task<PageList<Evento>> GetAllEventosAsync(PageParams pageParams, bool includePalestrantes = false);
       // Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
    }
}