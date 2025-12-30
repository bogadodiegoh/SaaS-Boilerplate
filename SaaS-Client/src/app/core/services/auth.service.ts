import { Injectable, signal, inject, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environment';
import { AuthResponse, LoginRequest, RegisterTenantRequest } from '../models/auth.model';
import { tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private readonly apiUrl = `${environment.apiUrl}/auth`;

  currentUser = signal<AuthResponse | null>(null);

  isAuthenticated = computed(() => !!this.currentUser());

  constructor() {
    const savedUser = localStorage.getItem('user_session');
    if (savedUser) {
      this.currentUser.set(JSON.parse(savedUser));
    }
  }

  login(credentials: LoginRequest) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => this.setSession(response))
    );
  }

  registerTenant(request: RegisterTenantRequest) {
    return this.http.post(`${this.apiUrl}/register-tenant`, request, {
        responseType: 'text' as 'json'
    });
  }

  logout() {
    localStorage.removeItem('user_session');
    this.currentUser.set(null);
    this.router.navigate(['/auth/login']);
  }

  private setSession(response: AuthResponse) {
    localStorage.setItem('user_session', JSON.stringify(response));
    this.currentUser.set(response);
  }

  getToken(): string | null {
    return this.currentUser()?.token ?? null;
  }
}