using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Happy.Command
{
    /// <summary>
    /// 事务拦截器。
    /// </summary>
    public sealed class TransactionAttribute : CommandInterceptorAttribute
    {
        /// <inheritdoc />
        public TransactionAttribute(int order) : base(order) { }

        /// <inheritdoc />
        public override void Intercept(ICommandExecuteContext context)
        {
            using (var ts = new TransactionScope())
            {
                context.ExecuteNext();

                ts.Complete();
            }
        }
    }
}
