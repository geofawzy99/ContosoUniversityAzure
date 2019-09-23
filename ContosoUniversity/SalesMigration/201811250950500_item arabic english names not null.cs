namespace ContosoUniversity.SalesMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemarabicenglishnamesnotnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Item", "ArDescription", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Item", "EnDescription", c => c.String(nullable: false, maxLength: 200, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Item", "EnDescription", c => c.String(maxLength: 200, unicode: false));
            AlterColumn("dbo.Item", "ArDescription", c => c.String(maxLength: 200));
        }
    }
}
