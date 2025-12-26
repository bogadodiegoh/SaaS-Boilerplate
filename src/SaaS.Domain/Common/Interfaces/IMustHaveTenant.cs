﻿namespace SaaS.Domain.Common.Interfaces
{
	public interface IMustHaveTenant
	{
		public string TenantId { get; set; }
	}
}
