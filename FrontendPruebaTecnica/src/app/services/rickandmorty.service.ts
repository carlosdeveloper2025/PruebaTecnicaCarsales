import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { episodio, PagedResponse } from '../models/modelos';


@Injectable({
  providedIn: 'root'
})
export class RickandmortyService {
  private appUrl = 'https://localhost:5000';
  private apiUrl = '/api/Values';
  private Url = '';

  constructor(private http: HttpClient) { }

  getEpisodios(page: number): Observable<PagedResponse<episodio>> {
    this.Url = this.appUrl + this.apiUrl + '?page=' + page;

    return this.http.get<PagedResponse<episodio>>(this.Url);

  }
}
