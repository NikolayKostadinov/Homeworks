namespace FileUpload.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTimeStampIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TraceLogMessages", new[] { "TimeStamp" });
            DropTable("dbo.TraceLogMessages");

            CreateTable(
                "dbo.TraceLogMessages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TimeStamp = c.DateTime(nullable: false),
                    UserName = c.String(),
                    Controller = c.String(),
                    Action = c.String(),
                    Message = c.String(),
                    Status = c.Int(nullable: false),
                });

            CreateIndex("dbo.TraceLogMessages",
                    new[] { "TimeStamp" },
                    clustered: true,
                    name: "IX_TimeStamp");

        }

        public override void Down()
        {
            DropIndex("dbo.TraceLogMessages", new[] { "TimeStamp" });
            DropTable("dbo.TraceLogMessages");
        }
    }
}