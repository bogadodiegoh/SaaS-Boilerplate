import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { LoginComponent } from './features/auth/pages/login/login.component';
import { Dashboard } from './features/dashboard/pages/dashboard/dashboard';
import { RegisterTenant } from './features/auth/pages/register-tenant/register-tenant';
import { Customers } from './features/dashboard/pages/customers/customers';
import { Home } from './features/dashboard/pages/home/home';

export const routes: Routes = [
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/register-tenant', component: RegisterTenant },
  { 
    path: 'dashboard', 
    component: Dashboard,
    canActivate: [authGuard],
    children: [
      { 
        path: '', 
        component: Home
      },
      { 
        path: 'customers', 
        component: Customers
      }
    ] 
  },
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
  { path: '**', redirectTo: 'auth/login' }
];
