﻿using System.Linq;
using System.Web.Mvc;

namespace T034.Tools.Attribute
{
    public class PermissionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var action = filterContext.ActionDescriptor.ActionName;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var role = MvcApplication.ActionRoles.FirstOrDefault(p => p.Action == action && p.Controller == controller);
            if (role == null) return;

            //if (filterContext.RequestContext.HttpContext.User.HasPermission(role.role)) return;
            var rolesCookie = filterContext.RequestContext.HttpContext.Request.Cookies["roles"];
            if (rolesCookie != null && rolesCookie.Value != null && rolesCookie.Value.Contains(role.Role))
                return;
            
            filterContext.Result = new RedirectResult("~/Errors/Unauthorized");

        }
    }

    public class Http403Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 403;
        }
    }
}