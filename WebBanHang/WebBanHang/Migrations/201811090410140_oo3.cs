namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oo3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDetail",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Properties = c.String(nullable: false, maxLength: 250),
                        Value = c.String(nullable: false, maxLength: 500),
                        ProductID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDetail", "ProductID", "dbo.Product");
            DropIndex("dbo.ProductDetail", new[] { "ProductID" });
            DropTable("dbo.ProductDetail");
        }
    }
}
