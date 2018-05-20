using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapePhoto.Filters
{
    public class GrapeAuthorizeFilter: ActionFilterAttribute
    {
        private const string UserSessionKeyName = "_UserName";
       
        
    }
}
