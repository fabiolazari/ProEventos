using System.Threading.Tasks;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Intefaces;

namespace ProEventos.Persistence.Services
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ProEventosContext _context;

        public BaseRepository(ProEventosContext context)
            => _context = context;
       
        public void Add<T>(T entity) where T : class
            => _context.Add(entity);

        public void Update<T>(T entity) where T : class
            => _context.Update(entity);

        public void Delete<T>(T entity) where T : class
            => _context.Remove(entity);

        public void DeleteRange<T>(T[] entityarray) where T : class
            => _context.RemoveRange(entityarray);

        public async Task<bool> SaveChangesAsync()
            => await _context.SaveChangesAsync() > 0;
    }
}