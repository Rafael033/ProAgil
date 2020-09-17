import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  eventos: any = [

    {
      EventosID: 1,
      Tema: 'Angular',
      Local: 'BH'
    },
    {
      EventosID: 2,
      Tema: 'API',
      Local: 'SP'
    },
    {
      EventosID: 3,
      Tema: '.NEt',
      Local: 'RJ'
    }
  ];

  constructor(private http: HttpClient) { }

  ngOnInit() {
this.getEventos();

  }
    getEventos(){
  this.eventos = this.http.get('http://localhost:5020/api/values').subscribe(response => {
    this.eventos = response;
  }, error => {
      console.log(error);
    });
    }


}
