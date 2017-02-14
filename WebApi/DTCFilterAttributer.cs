using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace WebApi {

    /// <summary>
    /// https://code.msdn.microsoft.com/Distributed-Transactions-c7e0a8c2
    /// </summary>
    public class DTCFilterAttributer : ActionFilterAttribute {


        private static readonly string TransactionID = "TransactionToken";

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);

            if (context.HttpContext.Request.Headers.ContainsKey(TransactionID)) {
                var values = context.HttpContext.Request.Headers[TransactionID];
                if (StringValues.IsNullOrEmpty(values)) {
                    var token = Convert.FromBase64String(values.First());
                    var trans = TransactionInterop.GetTransactionFromTransmitterPropagationToken(token);

                    var transactionScope = new TransactionScope(trans);

                    context.HttpContext.Items.Add(TransactionID, transactionScope);
                }
            }
        }


        public override void OnActionExecuted(ActionExecutedContext context) {
            base.OnActionExecuted(context);

            if (context.HttpContext.Items.ContainsKey(TransactionID)) {
                var tScope = (TransactionScope)context.HttpContext.Items[TransactionID];

                if (tScope != null) {
                    if (context.Exception != null) {
                        Transaction.Current.Rollback(context.Exception);
                    } else {
                        tScope.Complete();
                    }

                    tScope.Dispose();
                    context.HttpContext.Items.Remove(TransactionID);
                }
            }
        }
    }
}
