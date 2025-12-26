﻿using SaaS.Domain.Common;
using SaaS.Domain.Common.Interfaces;

namespace SaaS.Domain.Entities
{
    public class Product : BaseEntity, IMustHaveTenant
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public string TenantId { get; set; } = default!;
    }
}
