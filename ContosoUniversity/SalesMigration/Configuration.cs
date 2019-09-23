namespace ContosoUniversity.SalesMigration
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ContosoUniversity.DAL.SalesModel;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ContosoUniversity.DAL.SalesModel.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"SalesMigration";
            ContextKey = "ContosoUniversity.DAL.SalesModel.SalesContext";
        }

        protected override void Seed(ContosoUniversity.DAL.SalesModel.SalesContext context)
        {
            var authors = new List<Author>
            {
                new Author { Id =  1 , Name = "Bill Gates"},
                new Author { Id =  2 , Name = "Kim Kally"},
                new Author { Id =  3 , Name = "Jim Orleey"}
            };
            authors.ForEach(a => context.Authors.AddOrUpdate(a));
            context.SaveChanges();
        }
    }
}
