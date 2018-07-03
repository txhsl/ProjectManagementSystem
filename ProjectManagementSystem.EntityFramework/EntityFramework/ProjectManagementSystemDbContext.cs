using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using ProjectManagementSystem.Authorization.Roles;
using ProjectManagementSystem.Authorization.Users;
using ProjectManagementSystem.MultiTenancy;
using ProjectManagementSystem.Projects;
using ProjectManagementSystem.Modules;

namespace ProjectManagementSystem.EntityFramework
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ProjectManagementSystemDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ProjectManagementSystemDbContext()
            : base("Default")
        {
        }

        public IDbSet<Project> Projects { get; set; }
        public IDbSet<Module> Modules { get; set; }
        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ProjectManagementSystemDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ProjectManagementSystemDbContext since ABP automatically handles it.
         */
        public ProjectManagementSystemDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public ProjectManagementSystemDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public ProjectManagementSystemDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
