using Abp.Application.Services;

namespace Notification.API
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class APIAppServiceBase : ApplicationService
    {
        protected APIAppServiceBase()
        {
            LocalizationSourceName = APIConsts.LocalizationSourceName;
        }
    }
}