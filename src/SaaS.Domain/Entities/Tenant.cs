﻿using SaaS.Domain.Common;

namespace SaaS.Domain.Entities
{
	public class Tenant: BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public string Identifier { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
	}
}
