using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers {
    [Route("api/[controller]")]
    public class TestController : Controller {
        [HttpGet]
        public IEnumerable<Test> Get() {
            using (var db = new TestDbContext()) {
                return db.Tests;
            }
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Test value) {
            using (var db = new TestDbContext()) {
                var m = db.Tests.SingleOrDefault(t => t.ID == id);
                if (m != null) {
                    m.Name = value.Name;
                    db.SaveChanges();
                }
            }
        }

        [HttpDelete("{id}")]
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
