using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            public DbSet<userData> userData { get; set; }
         
   
    }
}