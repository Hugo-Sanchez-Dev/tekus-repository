
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CatalogService } from '../../services/catalog.service';
import { Catalog } from '../../interfaces/catalog.interface';


@Component({
  selector: 'app-detail-catalog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './detail-catalog.component.html',
})
export class DetailCatalogComponent implements OnInit {
  private _catalogService = inject(CatalogService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);

  catalog: Catalog | null = null;
  isLoading = true;

  ngOnInit(): void {
    const id = this._route.snapshot.params['id'];
    this.loadCatalog(id);
  }

  loadCatalog(id: string) {
    this.isLoading = true;
    this._catalogService.getCatalogById(id).subscribe({
      next: (response) => {
        if (response.data) {
          this.catalog = response.data;
        } else {
          alert('Servicio no encontrado');
          this._router.navigate(['/catalogs']);
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error al cargar servicio:', err);
        alert('Error al cargar el servicio');
        this._router.navigate(['/catalogs']);
      }
    });
  }

  goBack() {
    this._router.navigate(['/catalogs']);
  }

  onEdit() {
    this._router.navigate(['/catalogs/edit', this.catalog?.id]);
  }
}
