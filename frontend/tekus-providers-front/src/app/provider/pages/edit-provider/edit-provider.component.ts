// provider-editar.component.ts
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CountryService } from '../../../countries/services/country.service';
import { ProviderService } from '../../services/provider.service';
import { Country } from '../../../countries/interfaces/country.interface';

@Component({
  selector: 'app-edit-provider',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-provider.component.html',
})
export class EditProviderComponent implements OnInit {
  private _fb = inject(FormBuilder);
  private _providerService = inject(ProviderService);
  private _countryService = inject(CountryService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);

  providerForm: FormGroup;
  countries: Country[] = [];
  isLoading = true;
  isSaving = false;
  providerId!: string;

  constructor() {
    this.providerForm = this._fb.group({
      id: ['', [Validators.required, Validators.maxLength(50)]],
      name: ['', [Validators.required, Validators.maxLength(200)]],
      nit: ['', [Validators.required, Validators.maxLength(50)]],
      email: ['', [Validators.email, Validators.maxLength(100)]],
      createdAt: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.providerId = this._route.snapshot.params['id'];
    this.loadCountries();
    this.loadProvider();
  }

  loadCountries() {
    this._countryService.getCountries().subscribe({
      next: (response) => {
        this.countries = response.data;
      },
      error: (err) => console.error('Error al cargar países:', err)
    });
  }

 loadProvider() {
    this.isLoading = true;
    this._providerService.getProviderById(this.providerId).subscribe({
      next: (response) => {
        this.providerForm.patchValue(response.data);
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error al cargar provider:', err);
        alert('Error al cargar el provider');
        this._router.navigate(['/providers']);
      }
    });
  }

  onSubmit() {
    if (this.providerForm.invalid) {
      this.providerForm.markAllAsTouched();
      return;
    }

    this.isSaving = true;
    this._providerService.updateProvider(this.providerId, this.providerForm.value).subscribe({
      next: () => {
        alert('Provider actualizado exitosamente');
        this._router.navigate(['/providers']);
      },
      error: (err) => {
        console.error('Error al actualizar provider:', err);
        alert('Error al actualizar el provider');
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
