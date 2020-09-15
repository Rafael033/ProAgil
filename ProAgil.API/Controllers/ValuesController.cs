using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.API.Data;
using ProAgil.API.Model;



namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly DataContext _context ;
        public ValuesController(DataContext context)
        {
            _context = context;

        }

    
    // GET api/values
    [HttpGet]
    public async Task<IActionResult> Get() //Task para chamadas sincronas 
    {
        try
        {
            var results = await _context.Eventos.ToListAsync(); //await não vai ter uma espera das chamadas sincronas

            return Ok(results);
            
        }
        catch (System.Exception)
        {
            
           return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");
        }
  //     return Ok(_context.Eventos.ToList());
    }

    //GET api/values/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
         try
        {
            var results = await _context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id); //await não vai ter uma espera das chamadas sincronas

            return Ok(results);
            
        }
        catch (System.Exception)
        {
            
           return this.StatusCode(StatusCodes.Status500InternalServerError,"Banco de dados Falhou");
        }
       
       
        /*

        comentado pois os dados vão retornar no sqlite, não direto no codigo 

        return new Evento[]{
                new Evento (){
                    EventoId = 1,
                    Tema = "Verde",
                    Local = "Alvarenga",
                    Lote = "2º lote",
                    QtdPessoas = 100,
                    DataEvento = DateTime.Now.AddDays(2).ToString ("dd/MM/yyyy")

                },
                     new Evento (){
                    EventoId = 2,
                    Tema = "Azul",
                    Local = "Represa",
                    Lote = "1º lote",
                    QtdPessoas = 350,
                    DataEvento = DateTime.Now.AddDays(4).ToString ("dd/MM/yyyy")

                }
            }.FirstOrDefault(x => x.EventoId == id);
    }
*/
    }
    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
}
