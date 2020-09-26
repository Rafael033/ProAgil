import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  _filtroLista: string;

  get filtroLista(): string {

   return this._filtroLista;
  }
  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

eventosFiltrados: any [];
   eventos: any = [] ;
   imgameLargura = 50;
   imgameMargem = 2;
   mostrarImagem = false;


// comentado pq a chamado é pelo sqlLite    {
//       EventosID: 1,
//       Tema: 'Angular',
//       Local: 'BH'
//     },
//     {
//       EventosID: 2,
//       Tema: 'API',
//       Local: 'SP'
//     },
//     {
//       EventosID: 3,
//       Tema: '.NEt',
//    //   Local: 'RJ'
//   //  }
//  // ];

  constructor(private http: HttpClient) { }

  ngOnInit() {
this.getEventos();

  }
  filtrarEvento(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
evento => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1    );

  }
  alternarImagem(){

    this.mostrarImagem = !this.mostrarImagem;
  }
    getEventos(){
  this.eventos = this.http.get('http://localhost:5020/api/values').subscribe(response => {
    this.eventos = response;
  }, error => {
      console.log(error);
    });
    }


}
