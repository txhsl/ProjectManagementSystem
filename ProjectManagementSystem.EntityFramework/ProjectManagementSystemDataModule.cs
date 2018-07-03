using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using ProjectManagementSystem.EntityFramework;

namespace ProjectManagementSystem
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(ProjectManagementSystemCoreModule))]
    public class ProjectManagementSystemDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProjectManagementSystemDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
