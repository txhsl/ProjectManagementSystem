using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace ProjectManagementSystem.EntityFramework.Repositories
{
    public abstract class ProjectManagementSystemRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ProjectManagementSystemDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ProjectManagementSystemRepositoryBase(IDbContextProvider<ProjectManagementSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class ProjectManagementSystemRepositoryBase<TEntity> : ProjectManagementSystemRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ProjectManagementSystemRepositoryBase(IDbContextProvider<ProjectManagementSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
