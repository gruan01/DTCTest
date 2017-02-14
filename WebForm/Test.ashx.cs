using Data2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm {
    /// <summary>
    /// Test 的摘要说明
    /// </summary>
    public class Test : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            context.Response.ContentType = "applicaton/json";
            var ctx = context.Request["ctx"] ?? "";
            this.Process(ctx);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }

        private void Process(string ctx) {
            using (var dtc = new DTCWrapper())
            using (var db = new TestDbContext()) {
                db.Logs.Add(new Data2.Models.Log() {
                    CreateOn = DateTime.Now,
                    Ctx = ctx
                });

                db.SaveChanges();
                dtc.Commit();
            }
        }
    }
}