
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ProviderService } from '../../services/provider.service';
import { Provider } from '../../interfaces/provider.interface';

@Component({
  selector: 'app-detail-provider',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './detail-provider.component.html',
})
export class DetailProviderComponent implements OnInit {
  private _providerService = inject(ProviderService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);

  provider: Provider | null = null;
  isLoading = true;

  ngOnInit(): void {
    const id = this._route.snapshot.params['id'];
    this.loadProvider(id);
  }

  loadProvider(id: string) {
    this.isLoading = true;
    this._providerService.getProviderById(id).subscribe({
      next: (response) => {
        if (response.data) {
          this.provider = response.data;
        } else {
          alert('Proveedor no encontrado');
          this._router.navigate(['/providers']);
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error al cargar provider:', err);
        alert('Error al cargar el provider');
        this._router.navigate(['/providers']);
      }
    });
  }

  goBack() {
    this._router.navigate(['/providers']);
  }

  onEdit() {
    this._router.navigate(['/providers/edit', this.provider?.id]);
  }
}
