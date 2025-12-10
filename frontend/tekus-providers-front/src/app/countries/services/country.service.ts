import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenericResponse } from '../../interfaces/generic-response.interface';
import { Country } from '../interfaces/country.interface';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7003/api';
  private providerUrl = `${this.apiUrl}/Country`;

  getCountries(): Observable<GenericResponse<Country[]>> {
    return this.http.get<GenericResponse<Country[]>>(this.providerUrl);
  }
}
