import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Customer, CustomerService } from '../../../../core/services/customer.service';
import { ToastService } from '../../../../core/services/toast.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-customers',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customers.html',
  styleUrl: './customers.scss',
})
export class Customers implements OnInit {
  private customerService = inject(CustomerService);
  private toast = inject(ToastService);

  // Estados de la UI
  customers = signal<Customer[]>([]);
  isLoading = signal(false);
  isSaving = signal(false);
  isEditing = false;
  editingId: number | null = null;

  searchTerm = signal('');

  // Formulario
  newName = '';
  newEmail = '';
  newPhone = '';

  filteredCustomers = computed(() => {
    const term = this.searchTerm().toLowerCase();
    if (!term) return this.customers();

    return this.customers().filter(c => 
      c.name.toLowerCase().includes(term) || 
      c.email.toLowerCase().includes(term) ||
      c.phone.includes(term)
    );
  });
  
  ngOnInit() {
    this.loadCustomers();
  }

  loadCustomers() {
    this.isLoading.set(true);
    this.customerService.getCustomers()
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (data) => this.customers.set(data),
        error: () => this.toast.error('Error loading customers')
      });
  }

  saveCustomer() {
    if (!this.newName || !this.newEmail || !this.newPhone) return;

    this.isSaving.set(true);
    const customerData: Customer = { 
      name: this.newName, 
      email: this.newEmail, 
      phone: this.newPhone 
    };

    if (this.isEditing && this.editingId) {
      // Lógica de Actualización
      this.customerService.updateCustomer(this.editingId, customerData)
        .pipe(finalize(() => this.isSaving.set(false)))
        .subscribe({
          next: () => {
            this.toast.success('Customer updated successfully');
            this.loadCustomers();
            this.cancelEdit();
          },
          error: () => this.toast.error('Failed to update customer')
        });
    } else {
      // Lógica de Creación
      this.customerService.createCustomer(customerData)
        .pipe(finalize(() => this.isSaving.set(false)))
        .subscribe({
          next: () => {
            this.toast.success('Customer registered successfully');
            this.loadCustomers();
            this.resetForm();
          },
          error: () => this.toast.error('Failed to register customer')
        });
    }
  }

  deleteCustomer(id: number | undefined) {
    if (!id) return;
    
    if (confirm('Are you sure you want to delete this customer?')) {
      this.customerService.deleteCustomer(id).subscribe({
        next: () => {
          this.toast.success('Customer deleted');
          this.customers.update(prev => prev.filter(c => c.id !== id));
        },
        error: () => this.toast.error('Error deleting customer')
      });
    }
  }

  editCustomer(customer: Customer) {
    this.isEditing = true;
    this.editingId = customer.id!;
    this.newName = customer.name;
    this.newEmail = customer.email;
    this.newPhone = customer.phone;
    window.scrollTo({ top: 0, behavior: 'smooth' }); // Mejora de UX: sube al formulario
  }

  cancelEdit() {
    this.isEditing = false;
    this.editingId = null;
    this.resetForm();
  }

  private resetForm() {
    this.newName = '';
    this.newEmail = '';
    this.newPhone = '';
  }
}