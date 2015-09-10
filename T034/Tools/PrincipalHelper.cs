using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace T034.Tools
{
    public static class PrincipalHelper
    {
        //public static bool HasPermission(this IPrincipal principal, string role)
        //{

        //    var claimPrincipal = principal as ClaimsPrincipal;

        //    if (claimPrincipal == null)
        //        return false;

        //    var permissions = claimPrincipal.Claims.FirstOrDefault(c => c.Type == "permissions");

        //    return permissions != null && permissions.Value.Split(',').Contains(role);
        //}

    }

}