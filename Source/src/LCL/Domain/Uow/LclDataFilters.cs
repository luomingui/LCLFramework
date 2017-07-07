using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Domain.Uow
{
    public static class LclDataFilters
    {
        /// <summary>
        /// "SoftDelete".
        /// Soft delete filter.
        /// Prevents getting deleted data from database.
        /// See <see cref="ISoftDelete"/> interface.
        /// </summary>
        public const string SoftDelete = "SoftDelete";

        /// <summary>
        /// "MustHaveTenant".
        /// Tenant filter to prevent getting data that is
        /// not belong to current tenant.
        /// </summary>
        public const string MustHaveTenant = "MustHaveTenant";

        /// <summary>
        /// "MayHaveTenant".
        /// Tenant filter to prevent getting data that is
        /// not belong to current tenant.
        /// </summary>
        public const string MayHaveTenant = "MayHaveTenant";

        /// <summary>
        /// Standard parameters of ABP.
        /// </summary>
        public static class Parameters
        {
            /// <summary>
            /// "tenantId".
            /// </summary>
            public const string TenantId = "tenantId";
        }
    }
}
