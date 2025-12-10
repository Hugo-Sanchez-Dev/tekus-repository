import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CountryService } from '../../../countries/services/country.service';
import { ProviderService } from '../../services/provider.service';

@Component({
  selector: 'app-create-provider',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-provider.component.html',
})
export class CreateProviderComponent {
  private _fb = inject(FormBuilder);
  private _providerService = inject(ProviderService);
  private _countryService = inject(CountryService);
  private _router = inject(Router);

  providerForm: FormGroup;
  countries: any[] = [];
  isLoading = false;
  isSaving = false;

  constructor() {
    this.providerForm = this._fb.group({
      name: ['', [Validators.required, Validators.maxLength(200)]],
      nit: ['', [Validators.required, Validators.maxLength(50)]],
      email: ['', [Validators.email, Validators.maxLength(100)]],
      createdAt: ['', Validators.required],
    });

    this.getCountries();
  }

  getCountries() {
    this.isLoading = true;
    this._countryService.getCountries().subscribe({
      next: (response) => {
        this.countries = response.data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error al cargar países:', err);
        alert('Error al cargar países');
        this.isLoading = false;
      }
    });
  }

  onSubmit() {
    if (this.providerForm.invalid) {
      this.providerForm.markAllAsTouched();
      return;
    }

    this.isSaving = true;
    this._providerService.createProvider(this.providerForm.value).subscribe({
      next: () => {
        alert('Proveedor creado exitosamente');
        this._router.navigate(['/providers']);
      },
      error: (err) => {
        console.error('Error al crear proveedor:', err);
        alert('Error al crear el proveedor');
        this.isSaving = false;
      }
    });
  }

  onCancel() {
    this._router.navigate(['/providers']);
  }

  hasError(field: string, error: string) {
    const control = this.providerForm.get(field);
    return control?.hasError(error) && control?.touched;
  }
}
