import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'auth-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './auth-login.component.html'
})
export class AuthLogin {
  private _authService = inject(AuthService)
  private _router = inject(Router);
  loginForm: FormGroup;

  showPassword = signal(false);
  isLoading = signal(false);

  constructor(private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      user: ['admin', [Validators.required]],
      password: ['tekusTest', [Validators.required, Validators.minLength(6)]]
    });
  }

  togglePassword() {
    this.showPassword.update(value => !value);
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);

    this._authService.login(this.loginForm.value).subscribe({
      next: (response) => {
        localStorage.setItem('token', response.token);

        this._router.navigate(['dashboard']);

        this.isLoading.set(false);
      },
      error: (err) => {
        this.isLoading.set(false);
        // Aquí podrías mostrar una alerta o un toast notification
        alert('Credenciales inválidas o error en el servidor');
      }
    });
  }

  hasError(field: string, error: string) {
    const control = this.loginForm.get(field);
    return control?.hasError(error) && control?.touched;
  }
}
