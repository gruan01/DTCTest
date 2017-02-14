using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WebForm {
    public class DTCWrapper : IDisposable {

        private static readonly string TransactionID = "TransactionToken";


        private TransactionScope Scope = null;
        private Transaction Trans = null;

        public DTCWrapper() {
            var context = HttpContext.Current;
            if (context.Request.Headers.AllKeys.Contains(TransactionID)) {
                var values = context.Request.Headers.GetValues(TransactionID);
                if (values != null && values.Any()) {
                    var token = Convert.FromBase64String(values.First());
                    this.Trans = TransactionInterop.GetTransactionFromTransmitterPropagationToken(token);
                    //var scope = new TransactionScope(trans, TransactionScopeAsyncFlowOption.Enabled);
                    this.Scope = new TransactionScope(this.Trans);
                }
            }
        }

        public void Commit() {
            if (this.Scope != null)
                this.Scope.Complete();
        }

        public void Rollback(Exception ex = null) {
            if (ex != null)
                this.Trans.Rollback(ex);
            else
                this.Trans.Rollback();
        }

        #region dispose
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~DTCWrapper() {
            this.Dispose(false);
        }


        private bool isDisposed = false;
        private void Dispose(bool flag) {
            if (flag && !isDisposed) {
                isDisposed = true;
                if (this.Trans != null) {
                    this.Trans.Dispose();
                    this.Trans = null;
                }

                if (this.Scope != null) {
                    this.Scope.Dispose();
                    this.Scope = null;
                }
            }
        }
        #endregion
    }
}