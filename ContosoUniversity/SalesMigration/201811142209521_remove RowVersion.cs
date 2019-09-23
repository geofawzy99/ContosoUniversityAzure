namespace ContosoUniversity.SalesMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeRowVersion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Item", "RowVersion", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Item", "RowVersion", c => c.DateTime(nullable: false));
        }
    }
}
