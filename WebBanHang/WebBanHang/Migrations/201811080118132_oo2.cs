namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oo2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Code", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.Product", "MetaTitle", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.Product", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AlterColumn("dbo.Product", "Description", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Producer", "MetaTitle", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.ProductCategory", "MetaTitle", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.Sales", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AlterColumn("dbo.NewsCategory", "MetaTitle", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.News", "MetaTitle", c => c.String(nullable: false, maxLength: 250, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "MetaTitle", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.NewsCategory", "MetaTitle", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Sales", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ProductCategory", "MetaTitle", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Producer", "MetaTitle", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Product", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "MetaTitle", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Product", "Code", c => c.String(maxLength: 10));
        }
    }
}
