import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterTenant } from './register-tenant';

describe('RegisterTenant', () => {
  let component: RegisterTenant;
  let fixture: ComponentFixture<RegisterTenant>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterTenant]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterTenant);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
