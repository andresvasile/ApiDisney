﻿using Microsoft.AspNetCore.Identity;

namespace ApiDisney.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}