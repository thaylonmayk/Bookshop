import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Autor } from '../app/forms/autores/autor';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AutoresService {
  private readonly apiUrl = `${environment.apiUrl}/autores`;

  constructor(readonly http: HttpClient) { }

  getAutores() {
    return this.http.get<Autor[]>(this.apiUrl);
  }

  addAutor(autor: Autor) {
    return this.http.post(this.apiUrl, autor);
  }

  editAutor(autor: Autor) {
    const url = `${this.apiUrl}/${autor.cod}`;
    return this.http.put(url, autor);
  }

  getAutorById(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.get<Autor>(url);
  }

  deleteAutor(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.delete(url);
  }
}
