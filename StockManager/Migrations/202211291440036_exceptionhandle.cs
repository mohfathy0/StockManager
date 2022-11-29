namespace StockManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exceptionhandle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        logDate = c.DateTime(nullable: false, precision: 0),
                        logThread = c.String(maxLength: 50, storeType: "nvarchar"),
                        logLevel = c.String(maxLength: 50, storeType: "nvarchar"),
                        ActionType = c.String(maxLength: 50, storeType: "nvarchar"),
                        ScreenName = c.String(maxLength: 50, storeType: "nvarchar"),
                        UserName = c.String(maxLength: 50, storeType: "nvarchar"),
                        UserId = c.Int(),
                        RecordName = c.String(maxLength: 50, storeType: "nvarchar"),
                        RecordId = c.Int(),
                        MethodName = c.String(maxLength: 50, storeType: "nvarchar"),
                        ClassName = c.String(maxLength: 255, storeType: "nvarchar"),
                        LineNumber = c.Int(),
                        logSource = c.String(maxLength: 300, storeType: "nvarchar"),
                        logMessage = c.String(maxLength: 4000, storeType: "nvarchar"),
                        exception = c.String(maxLength: 4000, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExceptionLogs");
        }
    }
}
