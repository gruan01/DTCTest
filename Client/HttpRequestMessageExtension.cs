using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Client {

    /// <summary>
    /// https://code.msdn.microsoft.com/Distributed-Transactions-c7e0a8c2
    /// </summary>
    public static class HttpRequestMessageExtension {
        public static void AddTransactionPropagationToken(this HttpRequestMessage request) {
            if (Transaction.Current != null) {
                var token = TransactionInterop.GetTransmitterPropagationToken(Transaction.Current);
                request.Headers.Add("TransactionToken", Convert.ToBase64String(token));
            }
        }


        public static void AddTransactionToken(this HttpClient client) {
            if (Transaction.Current != null) {
                var token = TransactionInterop.GetTransmitterPropagationToken(Transaction.Current);
                client.DefaultRequestHeaders.Add("TransactionToken", Convert.ToBase64String(token));
            }
        }
    }
}
