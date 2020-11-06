using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{

    
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilDataContext _context;

        public ProAgilRepository(ProAgilDataContext context)
        {
            _context = context;

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; // with(nolock)


        }

        /////Gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity); 
        }

        public void delete<T>(T entity) where T : class
        {
           _context.Remove(entity);
        }

             public void update<T>(T entity) where T : class
        {
             _context.Update(entity);
        }

          public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync())> 0; //se eu chamar o add delete ou update ele tem que chamar o Save
        }

   

     // Getts Eventos e Palestras

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedeSociais);

            if(includePalestrantes){
                query = query.AsNoTracking() //no lock
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking() //no lock
            .OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();

        }

        public async Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes)
        {
             IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedeSociais);

            if(includePalestrantes){
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking() //no lock
         //   .OrderByDescending(c => c.DataEvento)
             .Where(c => c.Id == EventoId);

          //  return await query.ToArrayAsync();
             return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
             IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedeSociais);

            if(includePalestrantes){
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking() //nolock
            .OrderByDescending(c => c.DataEvento)
             .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(int PalestranteId, bool includeEventos = false)
        {

            //crio uma query **************** Facço uma consulta na Tabela Palestrantes 
              IQueryable<Palestrante> query = _context.Palestrantes
            
            .Include(c => c.RedeSociais); //incluindo as redesSocias dos Palestrantes
           
           //condição para se eu quiser adicionar os Eventos
            if(includeEventos){
                query = query.AsNoTracking() // nolock
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking() // no lock
            .OrderBy(c => c.Nome) //ordernar por Nome
            .Where(p => p.Id == PalestranteId); // aqui eu pesquiso o parametro que eu passei na chamada do metodo
            

            return await query.ToArrayAsync(); //return Assync conforme class public Assync
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name ,bool includeEventos)
        {
              IQueryable<Palestrante> query = _context.Palestrantes
            
            .Include(c => c.RedeSociais); //incluindo as redesSocias dos Palestrantes
           
           //condição para se eu quiser adicionar os Eventos
            if(includeEventos){
                query = query.AsNoTracking() // no lock
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking() //nolock
            .OrderBy(c => c.Nome) //ordernar por Nome
            .Where(p => p.Nome.ToLower().Contains(name.ToLower())); // aqui eu pesquiso o parametro nome como se fosse um like no sql uppper
            

            return await query.ToArrayAsync(); //return Assync conforme class public Assync
        }

      
    }
}