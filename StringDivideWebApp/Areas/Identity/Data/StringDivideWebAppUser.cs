using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StringDivideWebApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the StringDivideWebAppUser class
public class StringDivideWebAppUser : IdentityUser
{
    public string Firstname { get; set; }
    public string  Lastname { get; set; }
}

