namespace SaaS.Application.DTOs;

public record AuthResponse(
    string Token, 
    string Email, 
    string FirstName, 
    string LastName, 
    string TenantId);
