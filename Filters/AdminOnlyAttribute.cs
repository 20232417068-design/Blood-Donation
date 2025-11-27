using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BloodDonation.Filters
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isAdminLoggedIn = context.HttpContext.Session.GetString("AdminLoggedIn") == "true";

            if (!isAdminLoggedIn)
            {
                // Redirect to Admin Login Page
                context.Result = new RedirectToActionResult("Login", "Admin", null);
            }
        }
    }
}
