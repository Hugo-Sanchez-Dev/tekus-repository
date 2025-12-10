import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CatalogService } from '../../services/catalog.service';


@Component({
  selector: 'app-create-catalog',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-catalog.component.html',
})
export class CreateCatalogComponent {
  private _fb = inject(FormBuilder);
  private _catalogService = inject(CatalogService);
  private _router = inject(Router);

  catalogForm: FormGroup;
  countries: any[] = [];
  isLoading = false;
  isSaving = false;

  constructor() {
    this.catalogForm = this._fb.group({
      name: ['', [Validators.required, Validators.maxLength(200)]],
      hourlyRate: [0, [Validators.required, Validators.maxLength(50)]],
    });
  }

  onSubmit() {
    if (this.catalogForm.invalid) {
      this.catalogForm.markAllAsTouched();
      return;
    }

    this.isSaving = true;
    this._catalogService.createCatalog(this.catalogForm.value).subscribe({
      next: () => {
        alert('Servicio creado exitosamente');
        this._router.navigate(['/catalogs']);
      },
      error: (err) => {
        console.error('Error al crear Servicio:', err);
        alert('Error al crear el Servicio');
        this.isSaving = false;
      }
    });
  }

  onCancel() {
    this._router.navigate(['/catalogs']);
  }

  hasError(field: string, error: string) {
    const control = this.catalogForm.get(field);
    return control?.hasError(error) && control?.touched;
  }
}
