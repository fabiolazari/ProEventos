using System.Threading.Tasks;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence.Intefaces
{
    public interface IEventoRepository
    {
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
        Task<PageList<Evento>> GetAllEventosAsync(PageParams pageParams, bool includePalestrantes = false);
        //Task<PageList<Evento>> GetAllEventosByTemaAsync(PageParams pageParams, string tema, bool includePalestrantes = false);
    }
}