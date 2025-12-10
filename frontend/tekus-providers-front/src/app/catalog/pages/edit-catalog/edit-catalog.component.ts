// catalog-editar.component.ts
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CatalogService } from '../../services/catalog.service';

@Component({
  selector: 'app-edit-catalog',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-catalog.component.html',
})
export class EditCatalogComponent implements OnInit {
  private _fb = inject(FormBuilder);
  private _catalogService = inject(CatalogService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);

  catalogForm: FormGroup;
  isLoading = true;
  isSaving = false;
  catalogId!: string;

  constructor() {
    this.catalogForm = this._fb.group({
      id: ['', [Validators.required, Validators.maxLength(50)]],
      name: ['', [Validators.required, Validators.maxLength(200)]],
      hourlyRate: [0, [Validators.required, Validators.maxLength(50)]],
    });
  }

  ngOnInit(): void {
    this.catalogId = this._route.snapshot.params['id'];
    this.loadCatalog();
  }

 loadCatalog() {
    this.isLoading = true;
    this._catalogService.getCatalogById(this.catalogId).subscribe({
      next: (response) => {
        this.catalogForm.patchValue(response.data);
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error al cargar sel servicio:', err);
        alert('Error al cargar el servicio');
        this._router.navigate(['/catalogs']);
      }
    });
  }

  onSubmit() {
    if (this.catalogForm.invalid) {
      this.catalogForm.markAllAsTouched();
      return;
    }

    this.isSaving = true;
    this._catalogService.updateCatalog(this.catalogId, this.catalogForm.value).subscribe({
      next: () => {
        alert('catalog actualizado exitosamente');
        this._router.navigate(['/catalogs']);
      },
      error: (err) => {
        console.error('Error al actualizar catalog:', err);
        alert('Error al actualizar el catalog');
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
