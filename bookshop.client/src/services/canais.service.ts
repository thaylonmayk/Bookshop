import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CanaisService {
  private readonly apiUrl = `${environment.apiUrl}/canais`;
  constructor(readonly http: HttpClient) { }

  getCanais() {
    return this.http.get(this.apiUrl);
  }

  addCanal(canal: any) {    
    return this.http.post(this.apiUrl, canal);
  }

  editCanal(canal: any) {
    const url = `${this.apiUrl}/${canal.cod}`;
    return this.http.put(url, canal);
  }

  getCanalById(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.get(url);
  }

  deleteCanal(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.delete(url);
  }
}
