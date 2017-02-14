using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi2 {
    public class DTCAttribute : ActionFilterAttribute {


        private static readonly string TransactionID = "TransactionToken";

        public override void OnActionExecuting(HttpActionContext context) {
            base.OnActionExecuting(context);

            if (context.Request.Headers.Contains(TransactionID)) {
                var values = context.Request.Headers.GetValues(TransactionID);
                if (values != null && values.Any()) {
                    var token = Convert.FromBase64String(values.First());
                    var trans = TransactionInterop.GetTransactionFromTransmitterPropagationToken(token);

                    var transactionScope = new TransactionScope(trans);

                    context.Request.Properties.Add(TransactionID, transactionScope);
                }
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext) {
            base.OnActionExecuted(actionExecutedContext);

            if (actionExecutedContext.Request.Properties.ContainsKey(TransactionID)) {
                var tScope = (TransactionScope)actionExecutedContext.Request.Properties[TransactionID];

                if (tScope != null) {
                    if (actionExecutedContext.Exception != null) {
                        Transaction.Current.Rollback(actionExecutedContext.Exception);
                    } else {
                        tScope.Complete();
                    }

                    tScope.Dispose();
                    actionExecutedContext.Request.Properties.Remove(TransactionID);
                }
            }
        }
    }
}