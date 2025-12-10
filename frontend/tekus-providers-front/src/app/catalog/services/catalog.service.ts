import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Catalog } from '../interfaces/catalog.interface';
import { GenericResponse } from '../../interfaces/generic-response.interface';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7003/api';
  private catalogUrl = `${this.apiUrl}/Catalog`;

  getCatalogs(): Observable<GenericResponse<Catalog[]>> {
    return this.http.get<GenericResponse<Catalog[]>>(this.catalogUrl);
  }

  getCatalogById(guid: string): Observable<GenericResponse<Catalog>> {
    return this.http.get<GenericResponse<Catalog>>(`${this.catalogUrl}/${guid}`);
  }

  createCatalog(catalog: Omit<Catalog, 'id'>): Observable<GenericResponse<Catalog>> {
    return this.http.post<GenericResponse<Catalog>>(this.catalogUrl, catalog);
  }

  updateCatalog(guid:string, catalog: Catalog): Observable<GenericResponse<Catalog>> {
    const urlWithId = `${this.catalogUrl}/${guid}`;
    return this.http.put<GenericResponse<Catalog>>(urlWithId, catalog);
  }

  deleteCatalog(id: string): Observable<any> {
    return this.http.delete(`${this.catalogUrl}/${id}`);
  }
}
