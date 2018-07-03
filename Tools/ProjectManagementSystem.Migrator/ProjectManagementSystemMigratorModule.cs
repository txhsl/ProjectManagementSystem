using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using ProjectManagementSystem.EntityFramework;

namespace ProjectManagementSystem.Migrator
{
    [DependsOn(typeof(ProjectManagementSystemDataModule))]
    public class ProjectManagementSystemMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<ProjectManagementSystemDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}