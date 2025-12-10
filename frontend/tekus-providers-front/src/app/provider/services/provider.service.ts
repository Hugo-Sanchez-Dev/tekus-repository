import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Provider } from '../interfaces/provider.interface';
import { GenericResponse } from '../../interfaces/generic-response.interface';

@Injectable({
  providedIn: 'root'
})
export class ProviderService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7003/api';
  private providerUrl = `${this.apiUrl}/Provider`;

  getProviders(): Observable<GenericResponse<Provider[]>> {
    return this.http.get<GenericResponse<Provider[]>>(this.providerUrl);
  }

  getProviderById(guid: string): Observable<GenericResponse<Provider>> {
    return this.http.get<GenericResponse<Provider>>(`${this.providerUrl}/${guid}`);
  }

  createProvider(provider: Omit<Provider, 'id'>): Observable<GenericResponse<Provider>> {
    return this.http.post<GenericResponse<Provider>>(this.providerUrl, provider);
  }

  updateProvider(guid: string, provider: Provider): Observable<GenericResponse<Provider>> {
    const urlWithId = `${this.providerUrl}/${guid}`;
    return this.http.put<GenericResponse<Provider>>(urlWithId, provider);
  }

  deleteProvider(guid: string): Observable<any> {
    return this.http.delete(`${this.providerUrl}/${guid}`);
  }
}
