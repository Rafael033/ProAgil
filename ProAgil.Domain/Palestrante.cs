using System.Collections.Generic;

namespace ProAgil.Domain
{
    public class Palestrante
    {
        
        public int Id {get; set;}
        public string Nome {get; set;}
        public string MiniCurriculo {get; set;}
        public string ImagemURL {get; set;}
        public string Telefone  {get; set;}
        public int Email {get; set;}
        public List<RedeSocial> RedeSociais {get; set;}

        public Evento Evento {get; set;}
        public List<PalestranteEvento> PalestranteEventos {get; set;}
    }
}