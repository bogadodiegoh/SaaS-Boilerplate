export interface AuthResponse {
  token: string;
  email: string;
  firstName: string;
  lastName: string;
  tenantId: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterTenantRequest {
  tenantId: string;
  companyName: string;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}