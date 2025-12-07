using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OegFlow.Domain
{
    public static class DefaultPermissions
    {
        public static readonly string[] All = new[]
        {
            "Organizations.Read.All",
            "Organizations.Read.Own",
            "Organizations.Create",
            "Organizations.Update.All",
            "Organizations.Update.Own",

            "Departments.Read.All",
            "Departments.Read.Organization",
            "Departments.Read.Own",

            "Requests.Create",
            "Requests.Approve",
            "Requests.Read.All",
            "Requests.Read.Organization",
            "Requests.Read.Own",
        };
    }
}
