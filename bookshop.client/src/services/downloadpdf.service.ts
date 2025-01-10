import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DownloadpdfService {
  private readonly apiUrl = `${environment.apiUrl}/relatorios`;

  constructor(private readonly http: HttpClient) { }

  getPdf(): Observable<any> {
    return this.http.get(this.apiUrl, {
      responseType: "arraybuffer",
    }).pipe(
      catchError((error) => {
        return throwError(() => new HttpErrorResponse({ status: 400, statusText: 'Bad Request' }));
      })
    );
  }
}