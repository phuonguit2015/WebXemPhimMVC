﻿using System;
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

        public System.Data.Entity.DbSet<WebXemPhim.Models.Ghe> Ghes { get; set; }

        public System.Data.Entity.DbSet<WebXemPhim.Models.PhongChieu> PhongChieux { get; set; }

        public System.Data.Entity.DbSet<WebXemPhim.Models.LoaiVe> LoaiVes { get; set; }

        public System.Data.Entity.DbSet<WebXemPhim.Models.LichChieu> LichChieux { get; set; }

        public System.Data.Entity.DbSet<WebXemPhim.Models.Ve> Ves { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<WebXemPhim.Models.NguoiDung> NguoiDungs { get; set; }
    }
}