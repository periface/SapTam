namespace Cinotam.AbpModuleZero.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectMember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Leader = c.Boolean(nullable: false),
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
                    { "DynamicFilter_ProjectMember_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProjectMembers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProjectMember_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
