namespace Insurance.Domain.Migrations.SystemADbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSystemADatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beneficiaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        InsurancePolicyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InsurancePolicies", t => t.InsurancePolicyId, cascadeDelete: true)
                .Index(t => t.InsurancePolicyId);
            
            CreateTable(
                "dbo.InsurancePolicies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        AgentName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Insurers", t => t.Id)
                .Index(t => t.Id);
            
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
            DropForeignKey("dbo.Beneficiaries", "InsurancePolicyId", "dbo.InsurancePolicies");
            DropForeignKey("dbo.InsurancePolicies", "Id", "dbo.Insurers");
            DropIndex("dbo.InsurancePolicies", new[] { "Id" });
            DropIndex("dbo.Beneficiaries", new[] { "InsurancePolicyId" });
            DropTable("dbo.Insurers");
            DropTable("dbo.InsurancePolicies");
            DropTable("dbo.Beneficiaries");
        }
    }
}
