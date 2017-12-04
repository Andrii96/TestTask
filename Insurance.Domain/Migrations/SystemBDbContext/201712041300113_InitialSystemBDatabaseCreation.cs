namespace Insurance.Domain.Migrations.SystemBDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSystemBDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InsurancePolicies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Long(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTill = c.DateTime(nullable: false),
                        AgentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.Insurers", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.AgentId);
            
            CreateTable(
                "dbo.Insurers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InsurancePolicies", "Id", "dbo.Insurers");
            DropForeignKey("dbo.InsurancePolicies", "AgentId", "dbo.Agents");
            DropIndex("dbo.InsurancePolicies", new[] { "AgentId" });
            DropIndex("dbo.InsurancePolicies", new[] { "Id" });
            DropTable("dbo.Insurers");
            DropTable("dbo.InsurancePolicies");
            DropTable("dbo.Agents");
        }
    }
}
