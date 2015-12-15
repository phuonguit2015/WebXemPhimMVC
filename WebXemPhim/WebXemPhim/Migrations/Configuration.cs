namespace WebXemPhim.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebXemPhim.DAL.MovieDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; // Tự động hóa migration
            AutomaticMigrationDataLossAllowed = true; // Cho phép thực hiện migration khi trường đó đã có dữ liệu
            ContextKey = "WebXemPhim.DAL.MovieDBContext";
        }

        protected override void Seed(WebXemPhim.DAL.MovieDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
