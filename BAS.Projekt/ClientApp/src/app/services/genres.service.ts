import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { IGenreList } from '../interfaces/genres/IGenreList';

@Injectable()

export class GenresService {

  url = 'http://localhost:50927/api/Genre';

  constructor(private http: HttpClient) { }

  getGenres(): Observable<IGenreList> {
    return this.http.get<IGenreList>(this.url + "/all");
  }
}
