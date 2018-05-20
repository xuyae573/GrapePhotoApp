using Abp.AspNetCore.Mvc.Controllers;

namespace Identity.API.Web.Controllers
{
    public abstract class APIControllerBase: AbpController
    {
        protected APIControllerBase()
        {
            LocalizationSourceName = APIConsts.LocalizationSourceName;
        }
    }
}