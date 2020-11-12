using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class RedeSocialDto
    {
        public int id{ get; set;}

        [Required (ErrorMessage="O Campo {0} è obrigatorio.")] // validações beck-end DataAnnotations 
        public string Nome {get; set;}
        public decimal URL {get; set;}

    }
}