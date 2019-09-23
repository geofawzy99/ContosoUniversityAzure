namespace ContosoUniversity.SalesMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true, storeType: "numeric"),
                        ArDescription = c.String(maxLength: 200),
                        Category = c.String(maxLength: 100),
                        EnDescription = c.String(maxLength: 200, unicode: false),
                        RowVersion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemStores",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true, storeType: "numeric"),
                        MinLimit = c.Int(nullable: false),
                        MaxLimit = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ItemId = c.Decimal(nullable: false, precision: 18, scale: 0, storeType: "numeric"),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.ItemId)
                .ForeignKey("dbo.Store", t => t.StoreId)
                .Index(t => t.ItemId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200, unicode: false),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Authors_Books",
                c => new
                    {
                        AuthorId = c.Int(nullable: false),
                        ItemId = c.Decimal(nullable: false, precision: 18, scale: 0, storeType: "numeric"),
                    })
                .PrimaryKey(t => new { t.AuthorId, t.ItemId })
                .ForeignKey("dbo.Author", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.SUV",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ManufactureYear = c.Int(),
                        BrandId = c.Int(),
                        ModelName = c.String(unicode: false),
                        SuvType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Salon",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ManufactureYear = c.Int(),
                        BrandId = c.Int(),
                        ModelName = c.String(unicode: false),
                        SalonType = c.Int(),
                        Description = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicine",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, storeType: "numeric"),
                        ScentificName = c.String(maxLength: 4000),
                        ExpireDate = c.DateTime(nullable: false),
                        MedicalGroup = c.String(maxLength: 8000, unicode: false),
                        BatchNo = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, storeType: "numeric"),
                        ISBN = c.String(nullable: false, maxLength: 12, unicode: false),
                        PublisherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.Id)
                .ForeignKey("dbo.Publisher", t => t.PublisherId)
                .Index(t => t.Id)
                .Index(t => t.PublisherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.Book", "Id", "dbo.Item");
            DropForeignKey("dbo.Medicine", "Id", "dbo.Item");
            DropForeignKey("dbo.Authors_Books", "ItemId", "dbo.Book");
            DropForeignKey("dbo.Authors_Books", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.ItemStores", "StoreId", "dbo.Store");
            DropForeignKey("dbo.ItemStores", "ItemId", "dbo.Item");
            DropIndex("dbo.Book", new[] { "PublisherId" });
            DropIndex("dbo.Book", new[] { "Id" });
            DropIndex("dbo.Medicine", new[] { "Id" });
            DropIndex("dbo.Authors_Books", new[] { "ItemId" });
            DropIndex("dbo.Authors_Books", new[] { "AuthorId" });
            DropIndex("dbo.ItemStores", new[] { "StoreId" });
            DropIndex("dbo.ItemStores", new[] { "ItemId" });
            DropTable("dbo.Book");
            DropTable("dbo.Medicine");
            DropTable("dbo.Salon");
            DropTable("dbo.SUV");
            DropTable("dbo.Authors_Books");
            DropTable("dbo.Publisher");
            DropTable("dbo.Store");
            DropTable("dbo.ItemStores");
            DropTable("dbo.Item");
            DropTable("dbo.Author");
        }
    }
}
