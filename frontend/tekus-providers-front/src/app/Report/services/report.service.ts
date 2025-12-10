import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenericResponse } from '../../interfaces/generic-response.interface';
import { CountryCatalogs } from '../interfaces/catalog-per-country.interface';
import { ProviderCatalogRanking } from '../interfaces/provider-catalog-rankin.interface';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7003/api';
  private providerUrl = `${this.apiUrl}/Report`;

  getProviderCatalogRanking(): Observable<GenericResponse<ProviderCatalogRanking[]>> {
    return this.http.get<GenericResponse<ProviderCatalogRanking[]>>(`${this.providerUrl}/ProviderCatalogRanking`);
  }

  getCatalogsPerCountry(): Observable<GenericResponse<CountryCatalogs[]>> {
    return this.http.get<GenericResponse<CountryCatalogs[]>>(`${this.providerUrl}/CatalogsPerCountry`);
  }
}
