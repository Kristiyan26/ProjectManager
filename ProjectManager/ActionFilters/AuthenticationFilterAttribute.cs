using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManager.Entities;
using ProjectManager.ExtentionMethods;

namespace ProjectManager.ActionFilters
{
    public class AuthenticationFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetObject<User>("loggedUser")==null)
            {
                context.Result = new RedirectResult("/Home/Login");

            }
        }
    }
}
