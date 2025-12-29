namespace SaaS.Domain.Exceptions;

public class TenantAlreadyExistsException : Exception
{
    public TenantAlreadyExistsException(string identifier) 
        : base($"La organización con el identificador '{identifier}' ya está registrada.") { }
}
