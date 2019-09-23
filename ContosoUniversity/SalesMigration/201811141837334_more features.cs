namespace ContosoUniversity.SalesMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class morefeatures : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Book", new[] { "PublisherId" });
            AddColumn("dbo.Book", "Langauge", c => c.Int(nullable: false));
            AddColumn("dbo.Item", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Book", "PublisherId", c => c.Int());
            CreateIndex("dbo.Book", "PublisherId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Book", new[] { "PublisherId" });
            AlterColumn("dbo.Book", "PublisherId", c => c.Int(nullable: false));
            DropColumn("dbo.Item", "Price");
            DropColumn("dbo.Book", "Langauge");
            CreateIndex("dbo.Book", "PublisherId");
        }
    }
}
