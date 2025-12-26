﻿using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SaaS.WebApi.Filters
{
	public class TenantHeaderFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			operation.Parameters =
			[
				new OpenApiParameter
				{
					Name = "x-tenant-id",
					In = ParameterLocation.Header,
					Required = false,
					Schema = new OpenApiSchema { Type = JsonSchemaType.String }
				}
			];
		}
	}
}
