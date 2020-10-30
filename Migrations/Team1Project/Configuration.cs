namespace Team_1_Project.Migrations.Team1Project
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Team_1_Project.DAL.Team1ProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\Team1Project";
            ContextKey = "Team_1_Project.DAL.Team1ProjectContext";
        }

        protected override void Seed(Team_1_Project.DAL.Team1ProjectContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
