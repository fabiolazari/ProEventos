import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take, map } from 'rxjs/operators';
import { Evento } from '../models/Evento';
import { PaginatedResult } from '@app/models/pagination';

@Injectable(
  //{ providedIn: 'root' }
)
export class EventoService {
  baseURL = 'https://localhost:44379/api/evento';

constructor(private http: HttpClient) { }

  public getEventos(page?: number, itemsPerPage?: number): Observable<PaginatedResult<Evento[]>> {

    const paginatedResult: PaginatedResult<Evento[]> = new PaginatedResult<Evento[]>();

    let params = new HttpParams;

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<Evento[]>(this.baseURL, { observe: 'response', params })
      .pipe(
        take(1),
        map((response) => {
          paginatedResult.result = response.body;

          if (response.headers.has('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }

          return paginatedResult;
        })
      );
  }

  public getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`)
    .pipe(take(1));
  }

  public getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`)
    .pipe(take(1));
  }

  public post(event: Evento): Observable<Evento> {
    return this.http.post<Evento>(this.baseURL, event)
    .pipe(take(1));
  }

  public put(event: Evento): Observable<Evento> {
    return this.http.put<Evento>(`${this.baseURL}/${event.id}`, event)
    .pipe(take(1));
  }

  public deleteEvent(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`)
    .pipe(take(1));
  }
}
