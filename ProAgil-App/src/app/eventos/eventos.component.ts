import { HttpClient } from '@angular/common/http';
import { ProviderAst } from '@angular/compiler';
import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../_models/Evento';

import { EventoService } from '../_services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  providers:   [EventoService]
})
export class EventosComponent implements OnInit {
  eventosFiltrados: Evento[];
   eventos: Evento[] ;
   imgameLargura = 50;
   imgameMargem = 2;
   mostrarImagem = false;
   modalRef: BsModalRef;

  _filtroLista: string ;

  constructor(private eventoService: EventoService
              ,private modalService: BsModalService) { }

  get filtroLista(): string {

   return this._filtroLista;
  }
  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  openModal(template: TemplateRef <any>){
    this.modalRef = this.modalService.show(template);
  }


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

 

  ngOnInit() {
this.getEventos();

  }

  filtrarEvento(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
evento => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1    );

  }
  alternarImagem(){

    this.mostrarImagem = !this.mostrarImagem;
  }
    getEventos(){
  this.eventoService.getAllEvento().subscribe(
    (_eventos: Evento[]) => {
    this.eventos = _eventos;
    this.eventosFiltrados = this.eventos;
    console.log(_eventos)
  }, error => {
      console.log(error);
    });
    }


}
