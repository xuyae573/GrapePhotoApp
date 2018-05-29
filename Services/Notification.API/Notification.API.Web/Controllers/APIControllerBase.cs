using Abp.AspNetCore.Mvc.Controllers;

namespace Notification.API.Web.Controllers
{
    public abstract class APIControllerBase: AbpController
    {
        protected APIControllerBase()
        {
            LocalizationSourceName = APIConsts.LocalizationSourceName;
        }
    }
}