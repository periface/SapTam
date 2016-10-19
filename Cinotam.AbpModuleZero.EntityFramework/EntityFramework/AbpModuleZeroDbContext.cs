using Abp.Zero.EntityFramework;
using Cinotam.AbpModuleZero.Authorization.Roles;
using Cinotam.AbpModuleZero.MultiTenancy;
using Cinotam.AbpModuleZero.Users;
using Cinotam.Cms.DatabaseEntities.Category.Entities;
using Cinotam.Cms.DatabaseEntities.Menus.Entities;
using Cinotam.Cms.DatabaseEntities.Pages.Entities;
using Cinotam.Cms.DatabaseEntities.Templates.Entities;
using SapModule.Core.BudgetInfo.Entities;
using SapModule.Core.Client.Entities;
using SapModule.Core.ProjectMembers.Entities;
using SapModule.Core.Projects.Entities;
using SapModule.Core.ToDo.Entities;
using SapModule.Core.ToDoLists.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace Cinotam.AbpModuleZero.EntityFramework
{
    public class AbpModuleZeroDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        #region CinotamCms
        public IDbSet<Page> Pages { get; set; }
        public IDbSet<Template> Templates { get; set; }

        public IDbSet<Content> Contents { get; set; }
        public IDbSet<Resource> Resources { get; set; }
        public IDbSet<Chunk> Chunks { get; set; }
        public IDbSet<Menu> Menus { get; set; }
        public IDbSet<MenuContent> MenuContents { get; set; }
        public IDbSet<MenuSection> MenuSections { get; set; }
        public IDbSet<MenuSectionContent> MenuSectionContents { get; set; }
        public IDbSet<MenuSectionItem> MenuSectionItems { get; set; }
        public IDbSet<MenuSectionItemContent> MenuSectionItemContents { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<CategoryContent> CategoryContents { get; set; }
        #endregion


        #region CinotamSap
        public IDbSet<ProjectMember> ProjectMembers { get; set; }
        public IDbSet<ClientCompany> ClientCompanies { get; set; }
        public IDbSet<ClientInfo> ClientInfos { get; set; }
        public IDbSet<Budget> Budgets { get; set; }
        public IDbSet<BudgetInput> BudgetInputs { get; set; }
        public IDbSet<BudgetOutput> BudgetOutputs { get; set; }
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<ToDoList> TodoLists { get; set; }
        public IDbSet<Todo> Todos { get; set; }
        public IDbSet<Discussion> Discussions { get; set; }
        public IDbSet<TodoFile> TodoFiles { get; set; }
        public IDbSet<HistoryStatus> HistoryStatuses { get; set; }
        #endregion
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public AbpModuleZeroDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in AbpModuleZeroDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of AbpModuleZeroDbContext since ABP automatically handles it.
         */
        public AbpModuleZeroDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public AbpModuleZeroDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
