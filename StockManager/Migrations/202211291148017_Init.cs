namespace StockManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionLogs",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    ActionDate = c.DateTime(nullable: false, precision: 0),
                    ScreenId = c.Int(nullable: false),
                    ActionType = c.Byte(nullable: false),
                    RecordId = c.Int(nullable: false),
                    RecordName = c.String(unicode: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.GlobalSettings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SettingName = c.String(unicode: false),
                    SettingValue = c.Binary(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserAccessProfileDetails",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserAccessProfileId = c.Int(nullable: false),
                    ScreenId = c.Int(nullable: false),
                    CanShow = c.Boolean(nullable: false),
                    CanOpen = c.Boolean(nullable: false),
                    CanAdd = c.Boolean(nullable: false),
                    CanEdit = c.Boolean(nullable: false),
                    CanDelete = c.Boolean(nullable: false),
                    CanPrint = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserAccessProfiles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(unicode: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Username = c.String(nullable: false, unicode: false),
                    Password = c.String(nullable: false, unicode: false),
                    UserType = c.Byte(nullable: false),
                    SettingsProfileId = c.Int(nullable: false),
                    ScreenProfileId = c.Int(),
                    Inactive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    ModifiedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(precision: 0),
                    ModifiedDate = c.DateTime(precision: 0),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserSettingsProfileProperties",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ProfileId = c.Int(nullable: false),
                    PropertyName = c.String(unicode: false),
                    PropertyValue = c.Binary(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserSettingsProfiles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(unicode: false),
                })
                .PrimaryKey(t => t.Id);
            Sql(@"INSERT INTO `stockmanager`.`users`
                    ( 
                    `Username`,
                    `Password`,
                    `UserType`,
                    `SettingsProfileId`,
                    `ScreenProfileId`,
                    `Inactive`,
                    `CreatedBy`,
                    `ModifiedBy`,
                    `CreatedDate`,
                    `ModifiedDate`)
                    VALUES
                    ( 
                    'admin',
                    '$argon2i$v=19$m=8192,t=3,p=1$01hU5uXYJVdKOgbLGc2p8A$w3N8jfDk98aUQVm2nyk/RcuzNDHAAdQ4rBtYDjb9VzQ',
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    '2021-05-27 17:56:56.693',
                    '2021-05-27 17:56:56.693');
                    "
                );
        }

        public override void Down()
        {
            DropTable("dbo.UserSettingsProfiles");
            DropTable("dbo.UserSettingsProfileProperties");
            DropTable("dbo.Users");
            DropTable("dbo.UserAccessProfiles");
            DropTable("dbo.UserAccessProfileDetails");
            DropTable("dbo.GlobalSettings");
            DropTable("dbo.ActionLogs");
        }
    }
}
