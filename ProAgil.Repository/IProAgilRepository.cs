using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
         void Add<T> (T entity) where T : class;
         void update<T> (T entity) where T : class;

         void delete<T> (T entity) where T : class;

         Task<bool> SaveChangesAsync();
         
         Task<Evento[]> GetAllEventoAsyncByTema(string tema,bool includePalestrantes);  //para return ToArrayAsync EVENTO[array]
         Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);  //para return ToArrayAsync EVENTO[array]
         Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes); //para return FirstOrDefaultAsync EVENTO


        Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name ,bool includeEventos);
         Task<Palestrante[]> GetAllPalestrantesAsync(int PalestranteId , bool includeEventos);
    }
}