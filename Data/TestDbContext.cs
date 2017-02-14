using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data {
    public class TestDbContext : DbContext {

        public DbSet<Test> Tests {
            get; set;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder o) {
            o.UseSqlServer("Data Source=.;Initial Catalog=Test;Integrated Security=True");
        }
    }
}
