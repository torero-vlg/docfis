﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using T034.Models;
using T034.Tools.Attribute;
using T034.ViewModel.AutoMapper;

namespace T034
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static IEnumerable<ActionRole> ActionRoles { get; private set; }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new PermissionFilterAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            AutoMapperWebConfiguration.Configure(Server);

            ActionRoles = GetActionRoles();
        }

        private IEnumerable<ActionRole> GetActionRoles()
        {
            var _assembly = Assembly.GetExecutingAssembly();

            IEnumerable<MethodInfo> methods = _assembly.GetTypes().
                            SelectMany(t => t.GetMethods())
                            .Where(m => m.GetCustomAttributes(typeof(RoleAttribute), true).Length > 0);

            var result = methods.Select(m => new ActionRole()
            {
                Action = m.Name,
                Role = ((RoleAttribute)m.GetCustomAttributes(typeof(RoleAttribute), true).FirstOrDefault()).Role,
                Controller = m.GetBaseDefinition().ReflectedType.Name.Replace("Controller", "")
            }).ToList();

            return result;
        }

    }
}