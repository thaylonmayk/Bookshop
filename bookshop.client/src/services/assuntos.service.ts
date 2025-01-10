import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AssuntosService {
  private readonly apiUrl = `${environment.apiUrl}/assuntos`;
  constructor(readonly http: HttpClient) { }

  getAssuntos() {
    return this.http.get(this.apiUrl);
  }

  addAssunto(assunto: any) {    
    return this.http.post(this.apiUrl, assunto);
  }

  editAssunto(assunto: any) {
    const url = `${this.apiUrl}/${assunto.cod}`;
    return this.http.put(url, assunto);
  }

  getAssuntoById(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.get(url);
  }

  deleteAssunto(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.delete(url);
  }
}
