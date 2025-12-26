﻿namespace SaaS.WebApi.Models;
public record ErrorDetails(int StatusCode, string Message, string? Details = null);
