namespace Cinotam.AbpModuleZero.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class TodoLists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Observations = c.String(),
                        StartAt = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        FinishedOn = c.DateTime(),
                        PunishUnfinishedOnDate = c.Boolean(nullable: false),
                        PunishConfig = c.Decimal(precision: 18, scale: 2),
                        MarginDays = c.Int(nullable: false),
                        FinishOnHighPriorityTaskDone = c.Boolean(nullable: false),
                        FinishOnAllTasksDone = c.Boolean(nullable: false),
                        CostByDay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Budget = c.Decimal(precision: 18, scale: 2),
                        UsedBudget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvailableBudget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnableWeekends = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        AllowTodoSelection = c.Boolean(nullable: false),
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
                    { "DynamicFilter_ToDoList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.Todoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TodoName = c.String(),
                        ResponsibleId = c.Long(),
                        DueDate = c.DateTime(),
                        ShowDiscussionToClient = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        TodoList_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Todo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToDoLists", t => t.TodoList_Id)
                .Index(t => t.TodoList_Id);
            
            CreateTable(
                "dbo.Discussions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Todo_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Discussion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Todoes", t => t.Todo_Id)
                .Index(t => t.Todo_Id);
            
            CreateTable(
                "dbo.TodoFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        FileUrl = c.String(),
                        IdServiceFile = c.String(),
                        MimeType = c.String(),
                        FileType = c.String(),
                        Name = c.String(),
                        SecondaryUrl = c.String(),
                        SourceType = c.Int(nullable: false),
                        ShowToClient = c.Boolean(nullable: false),
                        Icon = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Todo_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TodoFile_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Todoes", t => t.Todo_Id)
                .Index(t => t.Todo_Id);
            
            CreateTable(
                "dbo.HistoryStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusType = c.Int(nullable: false),
                        Observations = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Todo_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HistoryStatus_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Todoes", t => t.Todo_Id)
                .Index(t => t.Todo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todoes", "TodoList_Id", "dbo.ToDoLists");
            DropForeignKey("dbo.HistoryStatus", "Todo_Id", "dbo.Todoes");
            DropForeignKey("dbo.TodoFiles", "Todo_Id", "dbo.Todoes");
            DropForeignKey("dbo.Discussions", "Todo_Id", "dbo.Todoes");
            DropForeignKey("dbo.ToDoLists", "Project_Id", "dbo.Projects");
            DropIndex("dbo.HistoryStatus", new[] { "Todo_Id" });
            DropIndex("dbo.TodoFiles", new[] { "Todo_Id" });
            DropIndex("dbo.Discussions", new[] { "Todo_Id" });
            DropIndex("dbo.Todoes", new[] { "TodoList_Id" });
            DropIndex("dbo.ToDoLists", new[] { "Project_Id" });
            DropTable("dbo.HistoryStatus",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HistoryStatus_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.TodoFiles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TodoFile_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Discussions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Discussion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Todoes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Todo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ToDoLists",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ToDoList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
