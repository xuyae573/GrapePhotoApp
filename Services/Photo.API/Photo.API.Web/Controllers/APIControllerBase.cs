using Abp.AspNetCore.Mvc.Controllers;

namespace Photo.API.Web.Controllers
{
    public abstract class APIControllerBase: AbpController
    {
        protected APIControllerBase()
        {
            LocalizationSourceName = APIConsts.LocalizationSourceName;
        }
    }
}