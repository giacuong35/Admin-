using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Admin.Filters
{
    public class BackOfficeOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"]?.ToString();

            // Cho phép Account và Home đi qua
            if (string.Equals(controller, "Account", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(controller, "Home", StringComparison.OrdinalIgnoreCase))
            {
                base.OnActionExecuting(context);
                return;
            }

            var token = context.HttpContext.Session.GetString("AccessToken");
            var roleId = context.HttpContext.Session.GetInt32("RoleId");

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            if (roleId != 1 && roleId != 2)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}