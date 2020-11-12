using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //controla a controller kkk //responsavel por realizar a validação da datanootations // facilidade do .NET core
    public class EventoController : ControllerBase //herda tudo de controller base consigo trabalhar com http
    {

        private readonly IProAgilRepository _repo; //injeto por meio de uma interface //injection dependences

        private readonly IMapper _mapper;

        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _mapper = mapper;
    
            _repo = repo;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get() //Task para chamadas sincronas 
        {
            try
            {
              // var results = await _repo.GetAllEventoAsync(true); //await não vai ter uma espera das chamadas sincronas
              var eventos = await _repo.GetAllEventoAsync(true); // utlizando o mapeamento autoMapper
              var results = _mapper.Map<EventoDto[]>(eventos);
                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            //     return Ok(_context.Eventos.ToList());
        }


        // GET api/values //busca informações
        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId) //Task para chamadas sincronas 
        {
            try
            {
                var evento = await _repo.GetAllEventoAsyncById(EventoId, true); //await não vai ter uma espera das chamadas sincronas
                var results = _mapper.Map<EventoDto>(evento);
                return Ok(results);

            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            //     return Ok(_context.Eventos.ToList());
        }
        [HttpGet("getByTema/{tema}")] //busca informações
        public async Task<IActionResult> Get(string tema) //Task para chamadas sincronas 
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsyncByTema(tema, true); //await não vai ter uma espera das chamadas sincronas
                var results = _mapper.Map<EventoDto[]>(eventos);
                return Ok(results);

            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }
            //     return Ok(_context.Eventos.ToList());
        }

        [HttpPost] //Salva/Cadastra informações
        public async Task<IActionResult> Post(Evento model) //Task para chamadas sincronas 
        {
            try
            {

                var evento = _mapper.Map<Evento>(model); // utlizando o mapeamento

                _repo.Add(evento); //recebendo o evento do mapeamento

                if (await _repo.SaveChangesAsync())
                {
               //     return Created($"/api/evento/{model.Id}", model);
               return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(evento)); // com autoMapper
                }


            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
            //     return Ok(_context.Eventos.ToList());
        }
        [HttpPut("{EventoId}")] //update Atualiza infomaçoes
        public async Task<IActionResult> Put(int EventoId, EventoDto model) //Task para chamadas sincronas 
        {
            try
            {

                var evento = await _repo.GetAllEventoAsyncById(EventoId, false);
                if (evento == null) return NotFound(); //se a chamada encontrou o evento  
                _mapper.Map(model,evento);
                _repo.update(evento);

                if (await _repo.SaveChangesAsync())
                {
                //    return Created($"/api/evento/{model.Id}", model);
                return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(evento));
                }


            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou");
            }

            return BadRequest();
            //     return Ok(_context.Eventos.ToList());
        }

        [HttpDelete("{EventoId}")] // deleta informações  
        public async Task<IActionResult> Delete(int EventoId) // para chamadas sincronas
        {
            try
            {
                var evento = await _repo.GetAllEventoAsyncById(EventoId, false);
                if (evento == null) return NotFound();

                _repo.delete(evento);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Peidou");
            }

            return BadRequest();
            //     return Ok(_context.Eventos.ToList());
        }

    }
}