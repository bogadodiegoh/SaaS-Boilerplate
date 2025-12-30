import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Customer, CustomerService } from '../../../../core/services/customer.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.html'
})
export class Home implements OnInit {
  private customerService = inject(CustomerService);
  
  totalCustomers = signal(0);
  activeUsers = signal(1);
  recentCustomers = signal<Customer[]>([]);
  isLoading = signal(true);

  ngOnInit() {
    this.loadDashboardData();
  }

  loadDashboardData() {
    this.customerService.getCustomers().subscribe({
      next: (customers) => {
        this.totalCustomers.set(customers.length);
        const sorted = [...customers].sort((a, b) => (b.id || 0) - (a.id || 0));
        this.recentCustomers.set(sorted.slice(0, 3));

        setTimeout(() => this.isLoading.set(false), 500);
      },
      error: (error) => {
        this.isLoading.set(false);
        console.error('Error loading dashboard data', error);
      }
    });
  }
}
