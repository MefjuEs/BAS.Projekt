import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = `${environment.apiUrl}/api/User`;

  constructor(private http: HttpClient) { }

  getAll() {
      return this.http.get(`${this.url}/users`);
  }

  getById(id: number) {
      return this.http.get(`${this.url}/users/${id}`);
  }
}