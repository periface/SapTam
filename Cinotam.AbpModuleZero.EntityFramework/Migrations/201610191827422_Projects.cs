namespace Cinotam.AbpModuleZero.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Projects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TotalTodoList = c.Int(nullable: false),
                        FinishedTodoList = c.Int(nullable: false),
                        PublicAccessCode = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        ClientCompany_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Project_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientCompanies", t => t.ClientCompany_Id)
                .Index(t => t.ClientCompany_Id);
            
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Initial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Available = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Used = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Default = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Project_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Budget_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.BudgetInputs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Concept = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Budget_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BudgetInput_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Budgets", t => t.Budget_Id)
                .Index(t => t.Budget_Id);
            
            CreateTable(
                "dbo.BudgetOutputs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Concept = c.String(),
                        Type = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Budget_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BudgetOutput_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Budgets", t => t.Budget_Id)
                .Index(t => t.Budget_Id);
            
            CreateTable(
                "dbo.ClientCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ClientCompany_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Gender = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Observations = c.String(),
                        ProfilePicture = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        ClientCompany_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ClientInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientCompanies", t => t.ClientCompany_Id)
                .Index(t => t.ClientCompany_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ClientCompany_Id", "dbo.ClientCompanies");
            DropForeignKey("dbo.ClientInfoes", "ClientCompany_Id", "dbo.ClientCompanies");
            DropForeignKey("dbo.Budgets", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.BudgetOutputs", "Budget_Id", "dbo.Budgets");
            DropForeignKey("dbo.BudgetInputs", "Budget_Id", "dbo.Budgets");
            DropIndex("dbo.ClientInfoes", new[] { "ClientCompany_Id" });
            DropIndex("dbo.BudgetOutputs", new[] { "Budget_Id" });
            DropIndex("dbo.BudgetInputs", new[] { "Budget_Id" });
            DropIndex("dbo.Budgets", new[] { "Project_Id" });
            DropIndex("dbo.Projects", new[] { "ClientCompany_Id" });
            DropTable("dbo.ClientInfoes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ClientInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ClientCompanies",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ClientCompany_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BudgetOutputs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BudgetOutput_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BudgetInputs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BudgetInput_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Budgets",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Budget_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Projects",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Project_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
