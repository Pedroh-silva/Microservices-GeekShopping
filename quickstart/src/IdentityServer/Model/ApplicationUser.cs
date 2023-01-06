﻿using System.Security.AccessControl;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
