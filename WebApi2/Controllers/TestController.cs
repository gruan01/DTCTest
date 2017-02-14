using Data2;
using Data2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi2.Controllers
{

    [DTC]
    public class TestController : ApiController {
        [HttpGet]
        public IEnumerable<Test> Get() {
            using (var db = new TestDbContext()) {
                return db.Tests.ToList();
            }
        }

        public Test Get(int id) {
            using (var db = new TestDbContext()) {
                return db.Tests.SingleOrDefault(t => t.ID == id);
            }
        }

        [HttpPost]
        public void Post([FromBody]Test value) {
            using (var db = new TestDbContext()) {
                db.Tests.Add(value);
                db.SaveChanges();
            }
        }

        public void Put(int id, [FromBody]Test value) {
            using (var db = new TestDbContext()) {
                var m = db.Tests.SingleOrDefault(t => t.ID == id);
                if (m != null) {
                    m.Name = value.Name;
                    db.SaveChanges();
                }
            }
        }

        public void Delete(int id) {

            using (var db = new TestDbContext()) {
                var m = db.Tests.SingleOrDefault(t => t.ID == id);
                if (m != null) {
                    db.Tests.Remove(m);
                    db.SaveChanges();
                }

            }
        }
    }
}
