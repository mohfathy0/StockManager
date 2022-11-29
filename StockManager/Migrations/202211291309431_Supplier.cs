namespace StockManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Supplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.String(unicode: false),
                        SupplierName = c.String(unicode: false),
                        SupplierAddress = c.String(unicode: false),
                        BankName = c.String(unicode: false),
                        BankAddress = c.String(unicode: false),
                        Iban = c.String(unicode: false),
                        Swift = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Suppliers");
        }
    }
}
