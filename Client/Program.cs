using Data2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Client {

    enum SimTypes {
        Unkinw = 0,
        Commit = 1,
        Rollback = 2
    }

    class Program {
        static void Main(string[] args) {
            while (true) {
                Console.WriteLine("1 模拟提交， 2 模拟回滚");
                var i = Console.ReadLine();
                SimTypes t;
                Enum.TryParse<SimTypes>(i.Trim(), out t);
                if (t != SimTypes.Unkinw)
                    AA(t);
            }
        }

        private static void AA(SimTypes t) {
            var ctx1 = "";
            var ctx2 = "";

            using (var trans = new TransactionScope())
            using (var client = new HttpClient()) {
                client.AddTransactionToken();

                var g = Guid.NewGuid().ToString();
                Console.WriteLine("{0}", g);

                var rst = client.PostAsJsonAsync("http://localhost:19377/api/Test", new Test() {
                    Name = g,
                    CreatedOn = DateTime.Now
                }).Result;

                ctx1 = rst.Content.ReadAsStringAsync().Result;

                var form = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("ctx", g)
                });
                var a = client.PostAsync("http://localhost:14717/Test.ashx", form)
                    .Result;

                ctx2 = a.Content.ReadAsStringAsync().Result;

                if (t == SimTypes.Commit)
                    trans.Complete();
                else
                    Transaction.Current.Rollback(new Exception("TTT"));
            }

            Console.WriteLine(ctx1);
        }
    }
}
