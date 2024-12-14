using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace excel_reader.Context
{
    public class ExcelContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public ExcelContext(DbContextOptions<ExcelContext> options) : base(options) { }
    }
}