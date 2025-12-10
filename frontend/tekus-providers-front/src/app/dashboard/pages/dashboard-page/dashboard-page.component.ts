import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReportService } from '../../../Report/services/report.service';
import { ProviderCatalogRanking } from '../../../Report/interfaces/provider-catalog-rankin.interface';
import { GenericResponse } from '../../../interfaces/generic-response.interface';
import { CountryCatalogs } from '../../../Report/interfaces/catalog-per-country.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'dashboard-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard-page.component.html'
})
export class DashboardPage implements OnInit{
  private _router = inject(Router);
  private _reportService = inject(ReportService)

  public isLoading: boolean = false;
  public providerRanking:ProviderCatalogRanking[] = [];
  public catalogPerCountry:CountryCatalogs[] = [];

  ngOnInit(): void {
    this.getProviderCatalogRanking();
    this.getCatalogsPerCountry();
  }
  getProviderCatalogRanking() {
    this.isLoading = true;
    this._reportService.getProviderCatalogRanking().subscribe({
      next: (response: GenericResponse<ProviderCatalogRanking[]>) => {
        this.providerRanking = response.data;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        alert('Credenciales inválidas o error en el servidor');
      }
    });
  }

  getCatalogsPerCountry() {
    this.isLoading = true;
    this._reportService.getCatalogsPerCountry().subscribe({
      next: (response: GenericResponse<CountryCatalogs[]>) => {
        this.catalogPerCountry = response.data;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        alert('Credenciales inválidas o error en el servidor');
      }
    });
  }

  navigateToProviders() {
    this._router.navigate(['/providers']);
  }

  navigateToCatalogs() {
    this._router.navigate(['/catalogs']);
  }
}
