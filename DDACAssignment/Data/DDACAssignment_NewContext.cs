using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DDACAssignment.Models;

namespace DDACAssignment.Data
{
    public class DDACAssignment_NewContext : DbContext
    {
        public DDACAssignment_NewContext (DbContextOptions<DDACAssignment_NewContext> options)
            : base(options)
        {
        }

        public DbSet<DDACAssignment.Models.Songs> Songs { get; set; }
        public DbSet<DDACAssignment.Models.RecordingSession> RecordingSession { get; set; }
    }
}
