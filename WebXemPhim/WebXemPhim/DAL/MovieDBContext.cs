using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebXemPhim.Migrations;
using WebXemPhim.Models;

namespace WebXemPhim.DAL
{
    public class MovieDBContext : DbContext
    {
        public MovieDBContext() : base("MovieDBContext") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MovieDBContext, Configuration>());
        }

        public DbSet<LoaiPhim> LoaiPhims { get; set; }
        public DbSet<Phim> Phims { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}