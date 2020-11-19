using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Team_1_Project.Models;

namespace Team_1_Project.DAL
{
    public class Team1ProjectContext: DbContext
    {
        public Team1ProjectContext() : base("name = DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();  // note: this is all one line!
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<userData> userData { get; set; }

        public DbSet<coreValuesRecognition> coreValuesRecognitions { get; set; }
    }
}