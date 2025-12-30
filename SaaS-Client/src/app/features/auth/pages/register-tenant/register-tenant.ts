import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-register-tenant',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register-tenant.html',
  styleUrl: './register-tenant.scss',
})
export class RegisterTenant {
  private authService = inject(AuthService);
  private router = inject(Router);

  companyName = '';
  tenantId = '';
  firstName = '';
  lastName = '';
  email = '';
  password = '';

  isLoading = signal(false);
  errorMessage = signal('');

  onRegister() {
    this.isLoading.set(true);
    this.errorMessage.set('');

    const payload = {
      companyName: this.companyName,
      tenantId: this.tenantId.toLowerCase().trim(),
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      password: this.password
    };

    this.authService.registerTenant(payload).subscribe({
      next: () => {
        alert('¡Empresa creada con éxito! Ahora puedes ingresar con tus credenciales.');
        this.router.navigate(['/auth/login']);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.errorMessage.set(err.error?.title || 'Error al registrar. Verifica si el ID de empresa ya existe.');
      }
    });
  }
}
