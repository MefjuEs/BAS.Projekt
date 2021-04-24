import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { IGenreList } from '../interfaces/genres/IGenreList';
import { IPersonnelInSelectDTO } from '../interfaces/personnel/IPersonnelInSelectDTO';

@Injectable()
export class PersonnelService {

  url = 'http://localhost:50927/api/Personnel';

  constructor(private http: HttpClient) { }

  async getPersonnelToSelectList(numberOfItems: number, fullName: string) {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    
    let params = new HttpParams();
    params = params.append('numberOfItems', numberOfItems.toString());
    params = params.append('fullName', fullName);
    return await this.http.get<IPersonnelInSelectDTO[]>(`${this.url}/select`, {headers: headers, params: params}).toPromise();
  }
}
