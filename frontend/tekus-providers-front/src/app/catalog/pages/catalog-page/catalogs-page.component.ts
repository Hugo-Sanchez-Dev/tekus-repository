import { Component, inject, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { CatalogService } from '../../services/catalog.service';
import { Catalog } from '../../interfaces/catalog.interface';
import { GenericResponse } from '../../../interfaces/generic-response.interface';
import { Router } from '@angular/router';


@Component({
  selector: 'app-catalogs-page',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './catalogs-page.component.html',
})
export class CatalogsPage {
  private _catalogService = inject(CatalogService)
  private _router = inject(Router);
  isLoading = false;
  public catalogs: Catalog[] = [];

  ngOnInit(): void {
    this.getCatalogs();
  }

  getCatalogs() {
    this.isLoading = true;
    this._catalogService.getCatalogs().subscribe({
      next: (response: GenericResponse<Catalog[]>) => {
        this.catalogs = response.data;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        alert('Credenciales inválidas o error en el servidor');
      }
    });
  }

  onCreate(): void {
    this._router.navigate(['/catalogs/create']);
  }

  onDetail(catalog: Catalog): void {
    this._router.navigate(['/catalogs/detail', catalog.id]);
  }

  onEdit(catalog: Catalog): void {
    this._router.navigate(['/catalogs/edit', catalog.id]);
  }

  onDelete(id: string): void {
    if (confirm(`¿Estás seguro de que quieres eliminar este servicio?`)) {
      this.isLoading = true;
      this._catalogService.deleteCatalog(id).subscribe({
        next: () => {
          this.getCatalogs();
          this.isLoading = false;
        },
        error: (err) => {
          this.isLoading = false;
          alert('Credenciales inválidas o error en el servidor');
        }
      });
    }
  }
}


