namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Product", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.Product", "IsNew");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "IsNew", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Product", "Status", c => c.Int());
            DropColumn("dbo.Product", "CreateDate");
        }
    }
}
