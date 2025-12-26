﻿namespace SaaS.Application.DTOs;

public record TenantDto(Guid Id, string Name, string Identifier, bool IsActive);
