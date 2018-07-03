using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using ProjectManagementSystem.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace ProjectManagementSystem.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ProjectManagementSystem.EntityFramework.ProjectManagementSystemDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ProjectManagementSystem";

            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());//设置Sql生成器为Mysql的
        }

        protected override void Seed(ProjectManagementSystem.EntityFramework.ProjectManagementSystemDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
