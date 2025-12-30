import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ToastService {
  show = signal(false);
  message = signal('');
  type = signal<'success' | 'error'>('success');

  success(msg: string) {
    this.message.set(msg);
    this.type.set('success');
    this.display();
  }

  error(msg: string) {
    this.message.set(msg);
    this.type.set('error');
    this.display();
  }

  private display() {
    this.show.set(true);
    setTimeout(() => this.show.set(false), 3000);
  }
}