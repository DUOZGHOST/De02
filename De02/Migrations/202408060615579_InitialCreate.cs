namespace De02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoaiSPs",
                c => new
                    {
                        MaLoai = c.String(nullable: false, maxLength: 2),
                        TenLoai = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.MaLoai);
            
            CreateTable(
                "dbo.Sanphams",
                c => new
                    {
                        MaSP = c.String(nullable: false, maxLength: 6),
                        TenSP = c.String(nullable: false, maxLength: 30),
                        Ngaynhap = c.DateTime(nullable: false),
                        MaLoai = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.MaSP)
                .ForeignKey("dbo.LoaiSPs", t => t.MaLoai, cascadeDelete: true)
                .Index(t => t.MaLoai);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sanphams", "MaLoai", "dbo.LoaiSPs");
            DropIndex("dbo.Sanphams", new[] { "MaLoai" });
            DropTable("dbo.Sanphams");
            DropTable("dbo.LoaiSPs");
        }
    }
}
