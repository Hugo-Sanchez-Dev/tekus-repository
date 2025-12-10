import { Component, inject, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ProviderService } from '../../services/provider.service';
import { Provider } from '../../interfaces/provider.interface';
import { GenericResponse } from '../../../interfaces/generic-response.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-providers-page',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './providers-page.component.html',
})
export class ProviderPage implements OnInit {
  private _providerService = inject(ProviderService)
  private _router = inject(Router);
  isLoading = false;
  public providers: Provider[] = [];

  ngOnInit(): void {
    this.getProviders();
  }

  getProviders() {
    this.isLoading = true;
    this._providerService.getProviders().subscribe({
      next: (response: GenericResponse<Provider[]>) => {
        this.providers = response.data;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        alert('Credenciales inválidas o error en el servidor');
      }
    });
  }

  onCreate(): void {
    this._router.navigate(['/providers/create']);
  }

  onDetail(provider: Provider): void {
    this._router.navigate(['/providers/detail', provider.id]);
  }

  onEdit(provider: Provider): void {
    this._router.navigate(['/providers/edit', provider.id]);
  }

  onDelete(id: string): void {
    if (confirm(`¿Estás seguro de que quieres eliminar al proveedor con ID: ${id}?`)) {
      this.isLoading = true;
      this._providerService.deleteProvider(id).subscribe({
        next: () => {
          this.getProviders();
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

