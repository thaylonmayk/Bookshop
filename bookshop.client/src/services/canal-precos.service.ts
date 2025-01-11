import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CanalPrecosService {
  private readonly apiUrl = `${environment.apiUrl}/canalprecos`;
  constructor(readonly http: HttpClient) { }

  getCanalPrecos() {
    return this.http.get(this.apiUrl);
  }

  addCanalPrecos(canalPrecos: any) {    
    return this.http.post(this.apiUrl, canalPrecos);
  }

  editCanalPrecos(canalPrecos: any) {
    const url = `${this.apiUrl}/${canalPrecos.cod}`;
    return this.http.put(url, canalPrecos);
  }

  getCanalPrecosById(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.get(url);
  }

  deleteCanalPrecos(cod: number) {
    const url = `${this.apiUrl}/${cod}`;
    return this.http.delete(url);
  }
}
