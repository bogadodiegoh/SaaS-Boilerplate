import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../../core/services/auth.service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { Toast } from '../../../../shared/components/toast/toast';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet, Toast],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit{
  private router = inject(Router);
  userName = signal<string>('Usuario');
  tenantId = signal<string | null>(null);
  
  public authService = inject(AuthService);
  user = this.authService.currentUser;

  ngOnInit() {
    this.loadUserInfo();
  }

  loadUserInfo() {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        
        this.userName.set(payload.unique_name || payload.sub || 'Admin');
        this.tenantId.set(payload.tenantId || 'N/A');
      } catch (error) {
        console.error('Error decodificando el token:', error);
        this.logout();
      }
    }
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login']);
  }
}
