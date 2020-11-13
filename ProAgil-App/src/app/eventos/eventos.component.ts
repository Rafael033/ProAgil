import { HttpClient } from '@angular/common/http';
import { ProviderAst, templateJitUrl } from '@angular/compiler';
import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { from } from 'rxjs';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';
import { ToastrService } from 'ngx-toastr';




@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  providers:   [EventoService]
})
export class EventosComponent implements OnInit {
   titulo = 'Eventos';
   eventosFiltrados: Evento[];
   eventos: Evento[] ;
   modoSalvar = 'post';
   evento: Evento ;

   imgameLargura = 50;
   imgameMargem = 2;
   mostrarImagem = false;
   registerForm: FormGroup;
   bodyDeletarEvento = '';
   apiURL = 'http://localhost:5020/api/evento/4';

  _filtroLista = '';

  constructor(private eventoService: EventoService
              ,private modalService: BsModalService
              ,private fb: FormBuilder
              ,private toastr: ToastrService
              ,private http: HttpClient
              ) { }

  get filtroLista(): string {

   return this._filtroLista;
  }
  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  editarEvento(evento: Evento , template: any){

    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);

  }
  excluirEvento(evento: Evento, template: any) {
    template.show();
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
  }

  novoEvento(template: any){
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  openModal(template: any){
    this.registerForm.reset(); //toda vez que abrir o modal limpa tudo
    template.show(template);
  }

  //chamando evento.service.ts
  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
        this.toastr.success('Deletado com Sucesso');
      }, error => {
        this.toastr.error('Erro ao tentar Deletar');
        console.log(error);
      }
    );
  }

/*
essa forma é de fazer a chamada na api por aqui mesmo
confirmeDelete() {
  this.http.delete(`${ this.apiURL }`)
            .subscribe(
              resultado => {
                console.log('Evento excluído com sucesso.');
              },
              erro => {

                  console.log('Evento não localizado.');

              }
            );
            }
            */
// comentado pq a chamada é pelo sqlLite    {
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
this.validation();

  }

  filtrarEvento(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
evento => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1    );

  }
  alternarImagem(){

    this.mostrarImagem = !this.mostrarImagem;
  }
/*
  validation() {
    this.registerForm = new FormGroup({
        tema: new FormControl ('' , [Validators.required, Validators.minLength(4), Validators.maxLength(30)])
      , local: new FormControl ('' , Validators.required)
      , dataEvento: new FormControl ('' , Validators.required)
      , imagemURL: new FormControl ('' , Validators.required)
      , qtdPessoas: new FormControl ('' , [Validators.required, Validators.max(12000)])
      , telefone: new FormControl ('' , Validators.required)
      , email: new FormControl ('' , [Validators.required, Validators.email])
    })
  }
*/
//boas praticas
 validation() {
  this.registerForm = this.fb.group ({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]]
    , local: ['', Validators.required]
    , dataEvento: ['', Validators.required]
    , imagemURL: ['', Validators.required]
    , qtdPessoas: ['', [Validators.required, Validators.max(12000)]]
    , telefone: ['', Validators.required]
    , email: ['', [Validators.required, Validators.email]]
  })
}

salvarAlteracao(template: any) {
  if (this.registerForm.valid) {
    if (this.modoSalvar === 'post') {
      this.evento = Object.assign({}, this.registerForm.value);

   //   this.uploadImagem();

      this.eventoService.postEvento(this.evento).subscribe(
        (novoEvento: Evento) => {
          template.hide();
          this.getEventos();
          this.toastr.success('Inserido com Sucesso!');
        }, error => {
          this.toastr.error(`Erro ao Inserir: ${error}`);
        }
      );
    } else {
      this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);

  //    this.uploadImagem();

      this.eventoService.putEvento(this.evento).subscribe(
        () => {
          template.hide();
          this.getEventos();
          this.toastr.success('Editado com Sucesso!');
        }, error => {
          this.toastr.error(`Erro ao Editar: ${error}`);
        }
      );
    }
  }
}





    getEventos() {
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

