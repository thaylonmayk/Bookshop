import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { LivroRequestDto } from '../app/dto/livro/livro';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LivrosService {
  private readonly apiUrl = `${environment.apiUrl}/livros`;
  
  constructor(readonly http: HttpClient) {}

  getLivros() {
    return this.http.get(this.apiUrl);
  }

  addLivro(livro: LivroRequestDto) {
    return this.http.post(this.apiUrl, livro);
  }

  editLivro(livro: LivroRequestDto) {
    const url = `${this.apiUrl}/${livro.cod}`;
    const livroToUpdate = { ...livro, anoPublicacao: livro.anoPublicacao.toString() };
    return this.http.put(url, livroToUpdate);
  }
  // editLivro(livro: LivroRequestDto): Observable<any> {
  //     const url = `${this.apiUrl}/${livro.cod}`;
  //     return this.http.put(url, livro).pipe(
  //       catchError((error) => {
  //         return throwError(() => new HttpErrorResponse({ status: 400, statusText: 'Bad Request' }));
  //       })
  //     );
  //   }

  getLivroById(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.get(url);
  }

  deleteLivro(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.delete(url);
  }
}
