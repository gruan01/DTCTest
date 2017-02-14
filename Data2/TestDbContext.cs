using Data2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Data2 {
    public class TestDbContext : DbContext {

        public DbSet<Test> Tests {
            get; set;
        }

        public DbSet<Log> Logs { get; set; }
    }
}
