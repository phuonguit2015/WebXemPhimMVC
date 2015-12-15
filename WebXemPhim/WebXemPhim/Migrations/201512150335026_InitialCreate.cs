namespace WebXemPhim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoaiPhim",
                c => new
                    {
                        LoaiPhimID = c.Int(nullable: false, identity: true),
                        TenLoaiPhim = c.String(),
                    })
                .PrimaryKey(t => t.LoaiPhimID);
            
            CreateTable(
                "dbo.Phim",
                c => new
                    {
                        PhimID = c.Int(nullable: false, identity: true),
                        TenPhim = c.String(),
                        DaoDien = c.String(),
                        DienVien = c.String(),
                        NoiDung = c.String(),
                        Poster = c.Binary(),
                        ThoiLuong = c.String(),
                        TrailerURL = c.String(),
                        TrangThai = c.Int(nullable: false),
                        NgayChieu = c.DateTime(nullable: false),
                        LoaiPhimID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhimID)
                .ForeignKey("dbo.LoaiPhim", t => t.LoaiPhimID, cascadeDelete: true)
                .Index(t => t.LoaiPhimID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phim", "LoaiPhimID", "dbo.LoaiPhim");
            DropIndex("dbo.Phim", new[] { "LoaiPhimID" });
            DropTable("dbo.Phim");
            DropTable("dbo.LoaiPhim");
        }
    }
}
