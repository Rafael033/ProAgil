using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
      [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase //herda tudo de controller base consigo trabalhar com http
    {

        private readonly IProAgilRepository _repo; //injeto por meio de uma interface //injection dependences

         public EventoController (IProAgilRepository repo){
             _repo = repo;
         }

           // GET api/values
    [HttpGet]
    public async Task<IActionResult> Get() //Task para chamadas sincronas 
    {
        try
        {
            var results = await _repo.GetAllEventoAsync(true); //await não vai ter uma espera das chamadas sincronas

            return Ok(results);
            
        }
        catch (System.Exception)
        {
            
           return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");
        }
  //     return Ok(_context.Eventos.ToList());
    }


          // GET api/values //busca informações
    [HttpGet ("{EventoId}")]
    public async Task<IActionResult> Get(int EventoId) //Task para chamadas sincronas 
    {
        try
        {
            var results = await _repo.GetAllEventoAsyncById(EventoId, true); //await não vai ter uma espera das chamadas sincronas

            return Ok(results);
            
        }
        catch (System.Exception)
        {
            
           return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");
        }
  //     return Ok(_context.Eventos.ToList());
    }
 [HttpGet ("getByTema/{tema}")] //busca informações
    public async Task<IActionResult> Get(string tema) //Task para chamadas sincronas 
    {
        try
        {
            var results = await _repo.GetAllEventoAsyncByTema(tema, true); //await não vai ter uma espera das chamadas sincronas

            return Ok(results);
            
        }
        catch (System.Exception)
        {
            
           return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");
        }
  //     return Ok(_context.Eventos.ToList());
    }

 [HttpPost] //Salva/Cadastra informações
    public async Task<IActionResult> Post(Evento model) //Task para chamadas sincronas 
    {
        try
        {
           _repo.Add(model);

           if( await _repo.SaveChangesAsync()){
               return Created($"/api/evento/{model.Id}",model);
           }

            
        }
        catch (System.Exception)
        {
            
           return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");
        }

        return BadRequest();
  //     return Ok(_context.Eventos.ToList());
    }
     [HttpPut] //update Atualiza infomaçoes
    public async Task<IActionResult> Put(int EventoId, Evento model) //Task para chamadas sincronas 
    {
        try
        {

            var evento = await _repo.GetAllEventoAsyncById (EventoId, false);
            if(evento == null) return NotFound(); //se a chamada encontrou o evento  

           _repo.update(model);

           if( await _repo.SaveChangesAsync()){
               return Created($"/api/evento/{model.Id}",model);
           }

            
        }
        catch (System.Exception)
        {
            
           return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");
        }

        return BadRequest();
  //     return Ok(_context.Eventos.ToList());
    }

 [HttpDelete] // deleta informações  
    public async Task<IActionResult> Delete(int EventoId) //Task para chamadas sincronas 
    {
        try
        {

            var evento = await _repo.GetAllEventoAsyncById (EventoId, false);
            if(evento == null) return NotFound();
           _repo.delete(evento);

           if( await _repo.SaveChangesAsync()){
               return Ok();
           }

            
        }
        catch (System.Exception)
        {
            
           return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");
        }

        return BadRequest();
  //     return Ok(_context.Eventos.ToList());
    }

    }
}