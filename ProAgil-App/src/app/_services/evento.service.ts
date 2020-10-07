import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseURl = 'http://localhost:5020/api/evento';
constructor(private http: HttpClient) {  }

getAllEvento(): Observable<Evento[]>{

  return this.http.get<Evento[]>(this.baseURl);

}

getEventoByTema(tema: string): Observable<Evento[]>{

  return this.http.get<Evento[]>('${this.baseURl}/getByTema/${tema}');

}
getEventoById(id: number): Observable<Evento>{

  return this.http.get<Evento>('${this.baseURl}/${id}');

}

}
