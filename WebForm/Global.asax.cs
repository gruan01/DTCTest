using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebForm {
    public class Global : System.Web.HttpApplication {

        private static readonly string TransactionID = "TransactionToken";

        protected void Application_Start(object sender, EventArgs e) {

        }

        protected void Session_Start(object sender, EventArgs e) {

        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            //var context = HttpContext.Current;
            //if (context.Request.Headers.AllKeys.Contains(TransactionID)) {
            //    var values = context.Request.Headers.GetValues(TransactionID);
            //    if (values != null && values.Any()) {
            //        var token = Convert.FromBase64String(values.First());
            //        var trans = TransactionInterop.GetTransactionFromTransmitterPropagationToken(token);

            //        var scope = new TransactionScope(trans, TransactionScopeAsyncFlowOption.Enabled);
            //        //var scope = new TransactionScope(trans);

            //        context.Items.Add(TransactionID, scope);
            //    }
            //}
        }

        protected void Application_EndRequest(object sender, EventArgs e) {
            //var context = HttpContext.Current;

            //if (context.Items.Contains(TransactionID)) {
            //    var tScope = (TransactionScope)context.Items[TransactionID];

            //    if (tScope != null) {
            //        if (context.Error != null) {
            //            Transaction.Current.Rollback(context.Error);
            //        } else {
            //            tScope.Complete();
            //        }

            //        //必须在创建 TransactionScope 时使用的同一线程上对其处置
            //        tScope.Dispose();

            //        context.Items.Remove(TransactionID);
            //    }
            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }

        protected void Application_Error(object sender, EventArgs e) {

        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}