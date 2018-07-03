using Abp.Web.Mvc.Views;

namespace ProjectManagementSystem.Web.Views
{
    public abstract class ProjectManagementSystemWebViewPageBase : ProjectManagementSystemWebViewPageBase<dynamic>
    {

    }

    public abstract class ProjectManagementSystemWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected ProjectManagementSystemWebViewPageBase()
        {
            LocalizationSourceName = ProjectManagementSystemConsts.LocalizationSourceName;
        }
    }
}