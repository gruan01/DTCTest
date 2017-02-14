using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi {
    public class DTCFilterAttributer : ActionFilterAttribute {


        private static readonly string TransactionID = "TransactionToken";

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);

            if (context.HttpContext.Request.Headers.ContainsKey(TransactionID)) {
                var values = context.HttpContext.Request.Headers[TransactionID];
                if (StringValues.IsNullOrEmpty(values)) {
                    var token = Convert.FromBase64String(values.First());
                    // 不支持
                    //TransactionInterop.GetTransactionFromTransmitterPropagationToken(transactionToken);
                }
            }
        }

    }
}
